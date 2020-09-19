using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSharpPlus
{
    public static class ObjectPlus
    {
        /// <summary>
        /// Copy values from source
        /// </summary>
        /// <param name="target"></param>
        /// <param name="source"></param>
        /// <param name="mapper">Indicate which source's property -> target's property</param>
        public static void CopyFrom(this object target, object source, Dictionary<string, string> mapper = null)
        {
            CopyValues(source, target, mapper);
        }

        /// <summary>
        /// Copy values to target
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="mapper">Indicate which source's property -> target's property</param>
        public static void CopyTo(this object source, object target, Dictionary<string, string> mapper = null)
        {
            CopyValues(source, target, mapper);
        }

        /// <summary>
        /// Copy values from source to target
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="mapper">Indicate which source's property -> target's property</param>
        private static void CopyValues(object source, object target, Dictionary<string, string> mapper = null)
        {
            if (source is null || target is null)
            {
                return;
            }

            var sourceProperties = GetProperties(source, x => x.CanRead);
            var targetProperties = GetProperties(target, x => x.CanWrite);

            if (mapper is null)
            {
                mapper = new Dictionary<string, string>();

                foreach (var sourceProperty in sourceProperties)
                {
                    var propertyName = sourceProperty.Key;

                    // Same name and same type
                    if (targetProperties.ContainsKey(propertyName) && targetProperties[propertyName].PropertyType == sourceProperty.Value.PropertyType)
                    {
                        // source's property == target's property
                        mapper[propertyName] = propertyName;
                    }
                }
            }

            foreach (var map in mapper)
            {
                var sourcePropertyName = map.Key;
                var targetPropertyName = map.Value;

                // Same type
                if (targetProperties[targetPropertyName].PropertyType == sourceProperties[sourcePropertyName].PropertyType)
                {
                    targetProperties[targetPropertyName].SetValue(target, sourceProperties[sourcePropertyName].GetValue(source));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        private static Dictionary<string, PropertyInfo> GetProperties(object obj, Func<PropertyInfo, bool> condition)
        {
            var result = new Dictionary<string, PropertyInfo>();

            obj.GetType().GetProperties().ToList().ForEach(x =>
            {
                if (condition(x))
                {
                    result[x.Name] = x;
                }
            });

            return result;
        }
    }
}
