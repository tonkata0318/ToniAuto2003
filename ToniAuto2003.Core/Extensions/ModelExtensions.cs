using System.Text.RegularExpressions;
using ToniAuto2003.Core.Contracts;

namespace ToniAuto2003.Core.Extensions
{
    public static class ModelExtensions 
    {
        public static string GetInformation(this ICarModel car)
        {
            string info= car.Make.Replace(" ", "-") + GetModel(car.Model);
            info=Regex.Replace(info, @"[^a-zA-Z0-9\-]", string.Empty);

            return info;

        }

        private static string GetModel(string model)
        {
            model=string.Join("-",model.Split(' ').Take(3));

            return model;
        }
    }
}
