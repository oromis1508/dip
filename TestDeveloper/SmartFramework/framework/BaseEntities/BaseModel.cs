using System.Linq;

namespace smart.framework.BaseEntities
{
    public class BaseModel
    {
        public override bool Equals(object obj) => GetType().GetFields().SequenceEqual(obj.GetType().GetFields());

        public override string ToString()
        {
            var result = "";
            foreach (var field in GetType().GetFields())
            {
                result += $"{field.Name}{field.GetValue(null)}";
            }

            return result;
        }
    }
}
