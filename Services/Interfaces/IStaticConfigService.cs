using SolbegTask2.Models;

namespace SolbegTask2.Services.Interfaces;

public interface IStaticConfigService
{
    public List<QuestionReward> GetQuestionRewardList();
}