using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EclassCDCD.ViewModels
{
	public class AnswerViewModel
	{
        public string AnswerID { get; set; }
        public string StudentID { get; set; }
        public int PlanID { get; set; }
        public string Time { get; set; }
        public string value { get; set; }
        public string questionId { get; set; }
    }
}
