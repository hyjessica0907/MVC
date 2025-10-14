using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Grade
    {
        [Key]
        [Display(Name = "學號")]
        public string StudentId { get; set; }

        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Display(Name = "系所")]
        public string Department { get; set; }

        [Display(Name = "年級")]
        public int GradeLevel { get; set; }

        [Display(Name = "信箱")]
        [EmailAddress(ErrorMessage = "請輸入有效的 Email 格式")]
        public string Email { get; set; }

        [Display(Name = "成績")]
        public int Score { get; set; }
    }
}
