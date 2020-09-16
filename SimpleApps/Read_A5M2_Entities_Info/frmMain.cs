using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Read_A5M2_Entities_Info
{
    public partial class frmMain : Form
    {
        /// <summary>
        /// Constructors
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
            treeView.AllowDrop = true;
            treeView.DragDrop += treeView_DragDrop;
            treeView.DragEnter += treeView_DragEnter;
        }

        ///// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (files.Length > 0)
            {
                treeView.Nodes.Clear();

                foreach (string file in files)
                {
                    FileInfo fileInfo = new FileInfo(file);

                    if (fileInfo.Extension == ".a5er")
                    {
                        ReadFile(fileInfo);
                        
                        // Read only the first file
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Move);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileInfo"></param>
        private void ReadFile(FileInfo fileInfo)
        {
            string fileContent = File.ReadAllText(fileInfo.FullName);

            Regex regex = new Regex(@"(?<=\[Entity\])(.|\n)*?(?=\[)");

            MatchCollection matchCollection = regex.Matches(fileContent);

            foreach (Match match in matchCollection)
            {
                treeView.Nodes.Add(Analysis(match.Value));
            }

            treeView.ExpandAll();
            
            if (treeView.Nodes.Count > 0)
            {
                treeView.Nodes[0].EnsureVisible();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private TreeNode Analysis(string value)
        {
            List<string> lstValues = value.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
            Entity entity = new Entity();
            List<PropertyInfo> properties = entity.GetType().GetProperties().ToList();

            foreach (PropertyInfo propertyInfo in properties)
            {
                object obj;

                if (propertyInfo.PropertyType.Name.StartsWith("List`"))
                {
                    obj = GetListPropertyValue(lstValues, propertyInfo.Name);
                }
                else
                {
                    obj = GetPropertyValue(lstValues, propertyInfo.Name);
                }

                propertyInfo.SetValue(entity, obj);
            }

            TreeNode treeNode = new TreeNode(entity.PName + " (" + entity.LName + ")");

            foreach(List<string> field in entity.Field)
            {
                treeNode.Nodes.Add(field[0] + " : " + field[1]); 
            }

            return treeNode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstValues"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private string GetPropertyValue(List<string> lstValues, string name)
        {
            return lstValues.Find(x => x.StartsWith(name)).Split('=')[1];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstValues"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private List<List<string>> GetListPropertyValue(List<string> lstValues, string name)
        {
            List <List<string>> lstResult = new List<List<string>>();

            foreach (string str in lstValues.FindAll(x => x.StartsWith(name)))
            {
                lstResult.Add(str.Split('=')[1].Split(',').ToList());
            }

            return lstResult;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    class Entity
    {
        public string PName { get; set; }
        public string LName { get; set; }
        public List<List<string>> Field { get; set; }
    }
}
