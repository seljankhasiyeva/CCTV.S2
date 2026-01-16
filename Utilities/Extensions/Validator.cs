using CCTV.S2.Utilities.Enums;

namespace CCTV.S2.Utilities.Extensions
{
    public static class Validator
    {
        public static bool ValidateType(this IFormFile formFile, string type)
        {
            if(formFile.ContentType.Contains(type))
            {
                return true;
            }
            return false;
        }

        public static bool ValidateSize(this IFormFile formFile, FileSize fileSize, int size)
        {
            switch(fileSize)
            {
                case FileSize.KB:
                    return formFile.Length <= size * 1024;
                case FileSize.MB:
                    return formFile.Length <= size * 1024 * 1024;
                case FileSize.GB:
                    return formFile.Length <= size * 1024 * 1024 * 1024;
            }
            return false;
        }


    }
}
