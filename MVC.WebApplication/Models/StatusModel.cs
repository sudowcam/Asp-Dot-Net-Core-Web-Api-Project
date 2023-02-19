using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC.WebApplication.Models
{
    public class StatusModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desciption { get; set; }
        public bool IsDeleted { get; set; }
        public List<string> Notes { get; set; }
    }
}
