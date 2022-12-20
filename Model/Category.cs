using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MM_MoneyFy.Model
{
    public class Category
    {
        public int Id { get; set; }
        public int iconId { get; set; }
        public Icon icon { get; set; }
        public string Name { get; set; }
        public  List<Operation> Operation { get; set; }
        public bool Type { get; set; }
        [NotMapped]
        public List<Operation> OperationsByCategoryId
        {
            get
            {
                return DataWorker.GetOperationsOfSelCategory(Id);
            }
        }
        [NotMapped]
        public string IconPath
        {
            get
            {
                return DataWorker.GetIconPathById(iconId);
            }
        }
    }
}
