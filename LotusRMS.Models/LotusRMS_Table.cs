using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotusRMS.Models
{
    public class LotusRMS_Table
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public Guid Id { get; set; }
        public string Table_Name { get;private set; }
        public int Table_No { get;private set; }
        public int No_Of_Chair { get; set; }
        public bool IsReserved { get; set; } = false;
        public Guid Table_Type_Id { get;private set; }
        [ForeignKey("Table_Type_Id")]
        public LotusRMS_Table_Type Table_Type { get; set; }
        public bool Status { get; set; }=true;
        public bool IsDelete { get; set; } = false;

        public LotusRMS_Table( string table_Name, int table_No,int no_Of_Chair, Guid table_Type_Id)
        {
            Table_Name = table_Name;
            Table_No = table_No;
            No_Of_Chair = no_Of_Chair;
            Table_Type_Id = table_Type_Id;
           
        }  public void Update( string table_Name, int table_No, int no_Of_Chair, Guid table_Type_Id)
        {
            Table_Name = table_Name;
            Table_No = table_No;

            No_Of_Chair = no_Of_Chair;
            Table_Type_Id = table_Type_Id;
           
        }   
    }
}
