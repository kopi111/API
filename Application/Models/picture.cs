using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class picture
    {
        public string fileName {  get; set; }

        public IFormFile formFile { get; set; }

        public picture(string fileName, IFormFile formFile)
        {
            this.fileName = fileName;
            this.formFile = formFile;
        }

        public picture() { }
    }
}
