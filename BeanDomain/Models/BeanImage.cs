using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BeanApp.Domain.Models
{
    public class BeanImage
    {
        [Key, Required]
        public int Id { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public string FileType { get; set; }
        public Bean Bean { get; set; }
        public string FileLocation { get; set; }

        public BeanImage() { }
        public BeanImage(string fileName, long fileSize, string fileType, string fileLocation)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException(fileName);
            }

            FileName = fileName;
            FileSize = fileSize;
            FileType = fileType;
            FileLocation = fileLocation;
        }
    }
}
