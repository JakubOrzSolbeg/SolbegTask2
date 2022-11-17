using System.Text.Json;
using SolbegTask2.Models;
using SolbegTask2.Services.Interfaces;

namespace SolbegTask2.Services;

public class StaticConfigService : IStaticConfigService
{
    private readonly List<QuestionReward> _questionRewards;

    public StaticConfigService()
    {
        using (StreamReader r = new StreamReader("Config/priceConfig.json"))
        {
            string json = r.ReadToEnd();
            var parsedConfig = JsonSerializer.Deserialize<List<QuestionReward>>(json);
            Console.WriteLine("Parsowanie plików z jsona");
            
            parsedConfig?.ForEach(e => Console.WriteLine($"Question {e.Price} {e.Milestone}"));
            
            Console.WriteLine(parsedConfig);
            if (parsedConfig == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                _questionRewards = parsedConfig;
            }
        }
    }

    public List<QuestionReward> GetQuestionRewardList()
    {
        return _questionRewards;
    }
}