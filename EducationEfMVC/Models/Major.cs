using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace EducationEfMVC.Models {

	public class Major {

		public int Id { get; set; }
		public string Description { get; set; }

		[JsonIgnore]
		public ICollection<Student> Students { get; set; }
	}
}