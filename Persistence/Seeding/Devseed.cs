using System.Threading.Tasks;
using Autofac;
using Dapper;
using Persistence.Configuration;

namespace Persistence.Seeding
{
    public static class Devseed
    {
        public static async Task Execute(IContainer container)
        {
            var connection = container.Resolve<DatabaseConnection>();

            await connection.Db.ExecuteAsync(@"
                INSERT INTO users(name, email, role, password, activated)
                VALUES('Erwin Schroedinger', 'admin@asg.com', '9001', '$2y$12$IF458i6pDVsY.XJR5OYd.OzjRPaPIsHmQhyE93E2V2SonZszVmzXm', true),
                      ('Johannes Kepler', 'admin2@asg.com', '9001', '$2y$12$D0lwJAXsCv8IzGfex0wV7uGdKj1nuYweTc6BnMZbS8xss/djGmySO', true),
                      ('Blaise Pascal', 'candidate@asg.com', '1001', '$2y$12$OZ6iLbvvFeMAPh.w8.ZLFetakRl9B5gA6MvCHgJs9kK2zCm1d5oFC', true),
                      ('Caroline Herschel', 'candidate2@asg.com', '1001', '$2y$12$hiSBt4DuQMZZAdlaG7osu.EE21jvHklqHbRUovC6qQZPDyCXOQNG2', true),
                      ('Edmond Halley', 'candidate3@asg.com', '1001', '$2y$12$E9LNbS1h2VZ/lErbbWunVOpA67QJrPmPCqCBuGuLKuMY/r.QjEjea', true),
                      ('Enrico Fermi', 'candidate4@asg.com', '1001', '$2y$12$YGA1lrJ7MlhgKtNU0qy4UOtrvlYpswFlMjVrqDdaFQLTS54/MYhyy', true);
            ");
        }
    }
}