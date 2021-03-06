﻿//    Copyright(C) 2020  Viet Do <https://github.com/doducviet>
//
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see <https://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Generate_Class_Files_From_Database
{
    public partial class frmMain : Form
    {
        private string outputFolder = String.Empty;

        private Database database = null;

        /// <summary>
        /// 
        /// </summary>
        public frmMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_Load(object sender, EventArgs e)
        {
            outputFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Output");

            // Initial
            Reset();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection cnn = new SqlConnection(tbConnectionString.Text.Trim());

                // Clear old data
                Reset();

                cnn.Open();

                #region Get table list

                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append(" SELECT");
                sbQuery.Append("  *");
                sbQuery.Append(" FROM");
                sbQuery.Append("  INFORMATION_SCHEMA.COLUMNS");
                sbQuery.Append(" ORDER BY ");
                sbQuery.Append("  TABLE_SCHEMA, TABLE_NAME, COLUMN_NAME"); // Change COLUMN_NAME to ORDINAL_POSITION if want the order as same as in table's design

                SqlCommand cmd = new SqlCommand(sbQuery.ToString(), cnn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                da.Fill(dataTable);
                da.Dispose();

                Table table = null;
                Column column = null;

                foreach (DataRow row in dataTable.Rows)
                {
                    string TABLE_SCHEMA = row["TABLE_SCHEMA"].ToString();
                    string TABLE_NAME = row["TABLE_NAME"].ToString();

                    if (!database.Tables.ContainsKey(TABLE_SCHEMA + "." + TABLE_NAME))
                    {
                        if (table != null && column != null)
                        {
                            table.Columns.Add(column);
                        }

                        table = new Table();
                        table.Schema = TABLE_SCHEMA;
                        table.Name = TABLE_NAME;
                        database.Tables[TABLE_SCHEMA + "." + TABLE_NAME] = table;

                        column = new Column(row);

                        clbTables.Items.Add(table.ToString(), true);
                    }
                    else
                    {
                        table.Columns.Add(column);

                        column = new Column(row);
                    }
                }

                if (column != null)
                {
                    table.Columns.Add(column);
                }

                #endregion

                cnn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                // Enable button Generate
                if (database.Tables.Count > 0)
                {
                    btnGenerate.Enabled = true;
                    btnGenerate.Focus();
                }
                else
                {
                    btnGenerate.Enabled = false;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>`
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            foreach (string item in clbTables.CheckedItems)
            {
                Table table = database.Tables[item];

                GenerateClassFile(table);
            }

            MessageBox.Show("- - - DONE - - -");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        private void GenerateClassFile(Table table)
        {
            string COMMENT;
            string FOLDER;
            string EXTENSION;
            string TAB = string.Empty.PadLeft(4);

            if (rdoCSharp.Checked)
            {
                COMMENT = "//";
                FOLDER = "CSharp";
                EXTENSION = ".cs";
            }
            else
            {
                COMMENT = "'";
                FOLDER = "VB";
                EXTENSION = ".vb";
            }

            string outputFolder = Path.Combine(this.outputFolder, FOLDER, table.Schema);

            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            StringBuilder sbOutput = new StringBuilder();
            sbOutput.AppendLine(COMMENT);
            sbOutput.AppendLine(COMMENT + " Generated by Generate_Class_Files_From_Database.exe at " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
            sbOutput.AppendLine(COMMENT + " Author : Viet Do");
            sbOutput.AppendLine(COMMENT + " More info : https://github.com/doducviet/SimpleApps/tree/dev/Generate_Class_Files_From_Database");
            sbOutput.AppendLine(COMMENT);
            sbOutput.AppendLine(string.Empty);

            if (rdoCSharp.Checked)
            {
                sbOutput.AppendLine("using System;");
                sbOutput.AppendLine("using System.Collections.Generic;");
                sbOutput.AppendLine("using System.Linq;");
                sbOutput.AppendLine("using System.Text;");
                sbOutput.AppendLine("using System.Threading.Tasks;");
                sbOutput.AppendLine(string.Empty);
                sbOutput.AppendLine("namespace " + table.Schema);
                sbOutput.AppendLine("{");
                sbOutput.AppendLine(TAB + "public class " + table.Name);
                sbOutput.AppendLine(TAB + "{");

                foreach (var column in table.Columns)
                {
                    string attributes = GetAttributesInfo(column);

                    if (!string.IsNullOrEmpty(attributes))
                    {
                        sbOutput.AppendLine(attributes);
                    }

                    sbOutput.AppendLine(TAB + TAB + "public " + GetCSharpDataType(column.DataType) + " " + column.Name + " { get; set; }");
                }

                sbOutput.AppendLine(TAB + "}");
                sbOutput.AppendLine("}");
            }
            else
            {
                StringBuilder sbPrivate = new StringBuilder();
                string dataType = string.Empty;
                string privateName = string.Empty;

                sbOutput.AppendLine("Namespace " + table.Schema);
                sbOutput.AppendLine(TAB + "Public Class " + table.Name);

                sbOutput.Append("<private defines>");

                foreach (var column in table.Columns)
                {
                    dataType = GetVBDataType(column.DataType);
                    privateName = "_" + column.Name.Substring(0, 1).ToLower() + column.Name.Substring(1);

                    sbPrivate.AppendLine(TAB + TAB + "Private " + privateName + " As " + dataType);

                    sbOutput.AppendLine(string.Empty);
                    sbOutput.AppendLine(TAB + TAB + "Public Property " + column.Name + "() As " + dataType);
                    sbOutput.AppendLine(TAB + TAB + TAB + "Get");
                    sbOutput.AppendLine(TAB + TAB + TAB + TAB + "Return " + privateName);
                    sbOutput.AppendLine(TAB + TAB + TAB + "End Get");
                    sbOutput.AppendLine(TAB + TAB + TAB + "Set");
                    sbOutput.AppendLine(TAB + TAB + TAB + TAB + privateName + " = Value");
                    sbOutput.AppendLine(TAB + TAB + TAB + "End Set");
                    sbOutput.AppendLine(TAB + TAB + "End Property");
                }

                sbOutput.AppendLine(TAB + "End Class");
                sbOutput.AppendLine("End Namespace");

                sbOutput.Replace("<private defines>", sbPrivate.ToString());
            }

            using (StreamWriter file = new StreamWriter(Path.Combine(outputFolder, table.Name + EXTENSION), false))
            {
                file.WriteLine(sbOutput.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        private string GetAttributesInfo(Column column)
        {
            if (column is null)
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();

            //if (column.Nullable)
            //{
            //    sb.AppendLine("\t\t[MaybeNull]");
            //}
            //else
            //{
            //    sb.AppendLine("\t\t[NotNull]");
            //}

            //if (!string.IsNullOrEmpty(column.DefaultValue))
            //{
            //    sb.AppendLine("\t\t[Default(\'" + column.DefaultValue + "\')]");
            //}

            //if (column.MaxLength != 0)
            //{
            //    sb.AppendLine("\t\t[MaxLength(" + column.MaxLength + ")]");
            //}

            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlDataType"></param>
        /// <returns></returns>
        private string GetCSharpDataType(string sqlDataType)
        {
            string result;

            switch (sqlDataType)
            {
                case "bit":
                    result = "bool";
                    break;
                case "tinyint":
                    result = "byte"; 
                    break;
                case "binary":
                case "filestream":
                case "image":
                case "rowversion":
                case "timestamp":
                case "varbinary":
                    result = "byte[]"; 
                    break;
                case "char":
                    result = "char";
                    break;
                case "date":
                case "datetime":
                case "datetime2":
                case "smalldatetime":
                    result = "DateTime";
                    break;
                case "datetimeoffset":
                    result = "DateTimeOffset";
                    break;
                case "decimal":
                case "money":
                case "numeric":
                case "smallmoney":
                    result = "decimal";
                    break;
                case "float":
                    result = "double";
                    break;
                case "uniqueidentifier":
                    result = "Guid";
                    break;
                case "int":
                    result = "int";
                    break;
                case "bigint":
                    result = "long";
                    break;
                case "geography":
                    result = "Microsoft.SqlServer.Types.SqlGeography";
                    break;
                case "geometry":
                    result = "Microsoft.SqlServer.Types.SqlGeometry";
                    break;
                case "hierarchyid":
                    result = "Microsoft.SqlServer.Types.SqlHierarchyId";
                    break;
                case "sql_variant":
                    result = "object";
                    break;
                case "smallint":
                    result = "short";
                    break;
                case "real":
                    result = "Single";
                    break;
                case "nchar":
                case "ntext":
                case "nvarchar":
                case "text":
                case "varchar":
                case "xml":
                    result = "string";
                    break;
                case "time":
                    result = "TimeSpan";
                    break;
                default:
                    result = "object";
                    break;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlDataType"></param>
        /// <returns></returns>
        private string GetVBDataType(string sqlDataType)
        {
            string result;

            switch (sqlDataType)
            {
                case "bit":
                    result = "Boolean";
                    break;
                case "tinyint":
                    result = "Byte";
                    break;
                //case "binary":
                //case "filestream":
                //case "image":
                //case "rowversion":
                //case "timestamp":
                //case "varbinary":
                //    result = "byte[]";
                //    break;
                case "char":
                    result = "Char";
                    break;
                case "date":
                case "datetime":
                case "datetime2":
                case "smalldatetime":
                    result = "DateTime";
                    break;
                case "datetimeoffset":
                    result = "DateTimeOffset";
                    break;
                case "decimal":
                case "money":
                case "numeric":
                case "smallmoney":
                    result = "Decimal";
                    break;
                case "float":
                    result = "Double";
                    break;
                //case "uniqueidentifier":
                //    result = "Guid";
                //    break;
                case "int":
                    result = "Integer";
                    break;
                case "bigint":
                    result = "Long";
                    break;
                //case "geography":
                //    result = "Microsoft.SqlServer.Types.SqlGeography";
                //    break;
                //case "geometry":
                //    result = "Microsoft.SqlServer.Types.SqlGeometry";
                //    break;
                //case "hierarchyid":
                //    result = "Microsoft.SqlServer.Types.SqlHierarchyId";
                //    break;
                case "sql_variant":
                    result = "Object";
                    break;
                case "smallint":
                    result = "Short";
                    break;
                case "real":
                    result = "Single";
                    break;
                case "nchar":
                case "ntext":
                case "nvarchar":
                case "text":
                case "varchar":
                case "xml":
                    result = "String";
                    break;
                case "time":
                    result = "TimeSpan";
                    break;
                default:
                    result = "Object";
                    break;
            }

            return result;
        }

        /// <summary>
        /// Select all tables
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckAll(clbTables, true);
        }

        /// <summary>
        /// Unselect all tables
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void unselectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CheckAll(clbTables, false);
        }

        /// <summary>
        /// Select/Unselect all items
        /// </summary>
        /// <param name="clb"></param>
        /// <param name="isCheck"></param>
        private void CheckAll(CheckedListBox clb, bool isCheck)
        {
            for (int i = 0; i < clb.Items.Count; i++)
            {
                clb.SetItemChecked(i, isCheck);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void Reset()
        {
            btnConnect.Enabled = true;
            btnGenerate.Enabled = false;

            database = new Database();
            clbTables.Items.Clear();
        }
    }

    class Database
    {
        public Dictionary<string, Table> Tables { get; set; }

        public Database()
        {
            Tables = new Dictionary<string, Table>();
        }
    }

    class Table
    {
        public string Schema { get; set; }
        public string Name { get; set; }
        public List<Column> Columns { get; set; }

        public Table()
        {
            Schema = string.Empty;
            Name = string.Empty;
            Columns = new List<Column>();
        }

        public Table(string schema, string name, List<Column> columns)
        {
            Schema = schema;
            Name = name;
            Columns = columns;
        }

        public override string ToString()
        {
            return Schema + "." + Name;
        }
    }

    class Column
    {
        public string Name { get; set; }
        public string DefaultValue { get; set; }
        public bool Nullable { get; set; }
        public string DataType { get; set; }
        public int MaxLength { get; set; }

        public Column(DataRow row)
        {
            Name = row["COLUMN_NAME"].ToString();

            if (row["COLUMN_DEFAULT"] != null)
            {
                DefaultValue = row["COLUMN_DEFAULT"].ToString()
                    .Replace("(", string.Empty)
                    .Replace(")", string.Empty)
                    .Replace("'", string.Empty);
            }

            Nullable = (row["IS_NULLABLE"].ToString() == "YES");
            DataType = row["DATA_TYPE"].ToString();

            if (row["CHARACTER_MAXIMUM_LENGTH"] != null && !string.IsNullOrEmpty(row["CHARACTER_MAXIMUM_LENGTH"].ToString()))
            {
                MaxLength = int.Parse(row["CHARACTER_MAXIMUM_LENGTH"].ToString());
            }
        }
    }
}
