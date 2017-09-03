using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SyntaxTool
{
    class TagToStandardize
    {
        public List<String> nameOfTag;
        public TagToStandardize(bool canNull, String nameOfTags)
        {
            if (nameOfTags != null)
            {
                this.nameOfTag = new List<string>();
                String[] sep = {"|"};
                this.nameOfTag.AddRange(nameOfTags.Split(sep, StringSplitOptions.RemoveEmptyEntries));
                if (canNull == true)
                {
                    this.nameOfTag.Add("");
                }
            }
        }
    }
}
