using System.Threading.Tasks;
using NUnit.Framework;

namespace TestDoubles.Spy
{
    public class SpyTests
    {
        [Test]
        public async Task Create_A_Joke_Use_Case_Should_Save_Good_Jokes()
        {
            var spyJokeRepository = new SpyJokeRepository();
            var useCase = new CreateAJokeUseCase(spyJokeRepository);
            var request = new CreateJokeRequest(
                "Anonymous",
                "Quelle partie du légume ne passe pas dans le mixer ? La chaise roulante"
            );

            await useCase.HandleAsync(request);

            spyJokeRepository
                .ShouldContain(request);
        }
    }
}