using ExpenseTracker.Dtos.GoalDtos;
using ExpenseTracker.Interfaces;
using ExpenseTracker.Interfaces.Service;

namespace ExpenseTracker.Services
{
    public class GoalService : IGoalService
    {
        private readonly IGoalRepository _goalRepository;
        private readonly IUserRepository _userRepository;

        public GoalService(IGoalRepository goalRepository, IUserRepository userRepository)
        {
            _goalRepository = goalRepository;
            _userRepository = userRepository;
        }

        public async Task<GoalDto> CreateGoalAsync(CreateGoalDto goalDto)
        {
            if (goalDto == null)
                throw new ArgumentNullException(nameof(goalDto));
            if (goalDto.Deadline < DateTime.Now)
                throw new ArgumentException("Cannot create a goal for a past date.");
            var user = await _userRepository.ReadEntity(goalDto.UserId);
            if (user == null)
                throw new ArgumentException("User does not exist.");
            var goal = goalDto.GetEntity(null);
            var createdGoal = await _goalRepository.CreateEntity(goal);

            return new GoalDto().GetDto(createdGoal);
        }

        public async Task<GoalDto?> GetGoalByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Goal ID cannot be empty.");

            var goal = await _goalRepository.ReadEntity(id);
            if (goal == null)
                return null;

            return new GoalDto().GetDto(goal);
        }

        public async Task<IEnumerable<GoalDto>> GetGoalsByUserIdAsync(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("User ID cannot be empty.");

            var user = await _userRepository.ReadEntity(userId);
            if (user == null)
                throw new ArgumentException("User does not exist.");

            var goals = await _goalRepository.GetGoalsByUserId(userId);
            return goals.Select(goal => new GoalDto().GetDto(goal));
        }

        public async Task<bool> UpdateGoalAsync(Guid id, UpdateGoalDto goalDto)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Goal ID cannot be empty.");

            if (goalDto == null)
                throw new ArgumentNullException(nameof(goalDto));

            var goal = await _goalRepository.ReadEntity(id);
            if (goal == null)
                throw new ArgumentException("Goal does not exist.");

            var updatedGoal = goalDto.GetEntity(goal);
            return await _goalRepository.UpdateEntity(id, updatedGoal);
        }
    }
}
