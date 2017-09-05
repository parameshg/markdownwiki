using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MDW.Models
{
    public class MediaListModel
    {
        public List<GroupModel> Groups { get; set; }

        public List<ImageModel> Images { get; set; }

        public MediaListModel()
        {
            Groups = new List<GroupModel>();

            Images = new List<ImageModel>();
        }
    }

    public class ImageModel
    {
        public string Name { get; set; }

        public string Group { get; set; }
    }

    public class CreateImageModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Group { get; set; }
    }

    public class DeleteImageModel
    {
        [Required]
        public string Name { get; set; }
    }
}