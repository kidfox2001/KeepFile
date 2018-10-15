namespace DataAccessSchool
{
    using System.Xml.Serialization;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Student : IValidatableObject
    {
        public int StudentID { get; set; }

        [Required(ErrorMessage="��ͧ�к�")]
        [MaxLength(6,ErrorMessage="�����ҡ���� 6")]
        public string Name { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DoB { get; set; }

        public byte[] Photo { get; set; }

        public decimal Height { get; set; }

        public float Weight { get; set; }

        [Range(0, 10, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int? GradeId { get; set; }

        public string LastName { get; set; }

        public string NickName { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Name == LastName)
            {
                yield return new ValidationResult("���� ���ʡ�ū��", new[] { "Name", "LastName" });
            }

            if (LastName .Length > 3)
            {
                yield return new ValidationResult("�ҡ���� 3", new[] {  "LastName" });
            }

        }
    }



}
