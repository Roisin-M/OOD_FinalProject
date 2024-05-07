using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using MainMenu;  // Ensure this is your namespace

namespace RoutineTests
{
    [TestFixture]
    public class RoutineTests
    {
        [Test]
        public void Test_AddRoutine_ChecksName()
        {
            List<Routine> routines = new List<Routine>();
            var routine = new Routine { Name = "Morning Yoga" };
            routines.Add(routine);
            Assert.That(routines, Has.Exactly(1).Matches<Routine>(r => r.Name == "Morning Yoga"));
        }

        [Test]
        public void Test_SortRoutines_ByLastUpdatedDescending()
        {
            List<Routine> routines = new List<Routine>
            {
                new Routine { Name = "Routine A", LastUpdated = DateTime.Now.AddHours(-1) },
                new Routine { Name = "Routine B", LastUpdated = DateTime.Now.AddHours(-2) },
                new Routine { Name = "Routine C", LastUpdated = DateTime.Now }
            };
            routines.Sort();
            Assert.That(routines[0].Name, Is.EqualTo("Routine C"));
            Assert.That(routines[1].Name, Is.EqualTo("Routine A"));
            Assert.That(routines[2].Name, Is.EqualTo("Routine B"));
        }



    }
}
