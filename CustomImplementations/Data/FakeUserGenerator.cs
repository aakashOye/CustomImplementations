using Bogus;
using CustomImplementations.Models;

namespace CustomImplementations.Data
{
    public static class FakeUserGenerator
    {
        public static FakeUser Generate()
        {
            var faker = new Faker<FakeUser>()
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.Phone, f => f.Phone.PhoneNumber());

            return faker.Generate();
        }
    }
}
