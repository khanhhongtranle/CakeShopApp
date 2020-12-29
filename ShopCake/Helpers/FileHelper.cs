using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopCake.Helpers
{
    class FileHelper
    {
        public FileHelper()
        {

        }

        public static string Copy(string source, string target)
        {
            string returnName = "";

            if (!File.Exists(target))
                File.Copy(source, target);
            else
            {
                for (int i = 1; ; ++i)
                {
                    String name = Path.Combine(
                      Path.GetDirectoryName(target),
                      Path.GetFileNameWithoutExtension(target) + String.Format("(copy{0})", i) +
                      Path.GetExtension(target));

                    if (!File.Exists(name))
                    {
                        File.Copy(source, name);
                        returnName = name;
                        break;
                    }
                }
            }
            return returnName;
        }
    }
}
