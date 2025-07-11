using Domain.Models.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Subject : BaseEntity<Guid>
    {
        public string Name { get; set; }

        public ICollection<Question> Questions { get; set; }
        public ExamConfiguration ExamConfiguration { get; set; }
    }
}
