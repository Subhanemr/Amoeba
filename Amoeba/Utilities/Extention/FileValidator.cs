namespace Amoeba.Utilities.Extention
{
    public static class FileValidator
    {
        public static bool IsValid(this IFormFile file, string fileType = "image/")
        {
            if (file.ContentType.Contains(fileType)) return true;
            return false;
        }
        public static bool LimitSize(this IFormFile file, int limitSize = 10)
        {
            if (file.Length <= limitSize * 1024 * 1024) return true;
            return false;
        }

        public static string GetGuidName(string fullName)
        {
            int score = fullName.LastIndexOf("_");
            if (score > 0)
            {
                string guidName = fullName.Substring(0, score);
                return guidName;
            }
            return fullName;
        }
        public static string GetFileFormat(string fullName)
        {
            int score = fullName.LastIndexOf(".");
            if (score > 0)
            {
                string fileFormat = fullName.Substring(score);
                return fileFormat;
            }
            return fullName;
        }

        public static async Task<string> CreateFileAsync(this IFormFile file, string root, params string[] folders)
        {
            string originalName = Guid.NewGuid().ToString() + "_" + file.FileName;
            string guidName = GetGuidName(originalName);
            string fileFormat = GetFileFormat(originalName);

            string fullName = guidName + fileFormat;

            string path = root;
            for (int i = 0; i < folders.Length; i++)
            {
                path = Path.Combine(path, folders[i]);
            }
            path = Path.Combine(path, fullName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return fullName;
        }

        public static async void DeleteAsync(this string fullName, string root, params string[] folders)
        {
            string path = root;
            for (int i = 0; i < folders.Length; i++)
            {
                path = Path.Combine(path, folders[i]);
            }
            path = Path.Combine(path, fullName);

            if (File.Exists(path)) File.Delete(fullName);
        }

        
    }
}
