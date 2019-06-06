namespace ContosoLearning.Data
{
    public static class VideoRepositoryFactory
    {
        public static IVideoRepository Create()
        {
            var repo = new VideoRepository(
                    CosmosDbAuthInfoFactory.Create()
                );
            return repo;
        }
    }
}
