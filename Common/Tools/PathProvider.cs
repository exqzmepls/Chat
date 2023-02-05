namespace Common.Tools
{
    public static class PathProvider
    {
        public static string GetMessageQueueLocalPath(string queueName)
        {
            var path = GetMessageQueuePath(".", queueName);
            return path;
        }

        public static string GetMessageQueuePath(string hostName, string queueName)
        {
            var path = $"{hostName}\\private$\\{queueName}";
            return path;
        }
    }
}