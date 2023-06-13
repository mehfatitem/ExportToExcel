using System.ComponentModel.DataAnnotations.Schema;

namespace FaceDetectService.Models
{
    [Table("detectoperation")]
    public class detectoperation
    {
        private int _id;
        private string _baseImage;
        private string _detectedImage;
        private long _operationTime;

        public int id { get => _id; set => _id = value; }
        public string baseImage { get => _baseImage; set => _baseImage = value; }
        public string detectedImage { get => _detectedImage; set => _detectedImage = value; }
        public long operationTime { get => _operationTime; set => _operationTime = value; }
    }
}
