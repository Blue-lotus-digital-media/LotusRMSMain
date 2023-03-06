using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Utility
{
    public static class TemplateGenerator
    {
        public static string GetHTMLString(List<QrTableVM> qrTables)
        {
            
            var sb = new StringBuilder();
            sb.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>

<div style='width:100%;display:flex;flex-wrap:wrap;'>                       
");
            foreach (var qrTable in qrTables)
            {
                sb.AppendFormat(@"
                        
                       <div style='height:100%; width:48%; padding:3px; display:flex; flex-direction:column;align-item:center;'>
<img src='{0}' width=450px height=450px style='margin:0 auto'>
<h2 align='center'>{1}</h2>
</div>
", qrTable.imageString, qrTable.Table_Name);
            }
            sb.Append(@"
                           
</div>     
                            </body>
                        </html>");
            return sb.ToString();
        }
    }
}
