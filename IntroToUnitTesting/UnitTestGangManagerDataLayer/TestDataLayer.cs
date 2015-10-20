using Microsoft.VisualStudio.TestTools.UnitTesting;
using GangManager;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace UnitTestGangManagerDataLayer
{
    [TestClass]
    public class TestDataLayer
    {
        [TestMethod]
        public void GivenMemberIdOf1MethodReturnsValidGangMember()
        {
            // Arrange     
            DataLayer dataLayer = new DataLayer();
            int memberID = 1;
            GangMember expectedMember = new GangMember()
            {
                MemberID = 1,
                MemberName = "Gentleman Jack",
                MemberRank = "Bootlegger"
            };

            // Act
            GangMember actualMember = dataLayer.GetGangMemberByMemberID(memberID);
            
            // Assert
            Assert.AreEqual(expectedMember.MemberID, actualMember.MemberID);
            Assert.AreEqual(expectedMember.MemberName, actualMember.MemberName);
            Assert.AreEqual(expectedMember.MemberRank, actualMember.MemberRank);
        }


        [TestMethod]
        public void GivenIdOf1ThenReturnAsynchronouslyGangMemberFromDbToMatchExpected()
        {
            // Arrange
            // Instantiate the DataLayer which we are to use.
            DataLayer dataLayer = new DataLayer();

            // Declare the actual gang member.
            GangMember actualMember = null;

            // Decide on which gang member we want from the database
            int memberID = 1;

            // Act
            // Create a task this goes and gets us the gang member we are after on 
            // a separate thread. Because I/O can take some time we can now move on
            // and do other stuff.
            Task<GangMember> actualMemberTask = dataLayer.GetGangMemberByIDAsync(memberID);

            // Arrange - This is arranging but in order to show the asyc behaviour, it has been implemented after the ACT
            // In the mean time while I/O is happening we can instantiate our 
            // expected gang member, this is what we will test against.
            GangMember expectedMember = new GangMember()
            {
                MemberID = 1,
                MemberName = "Gentleman Jack",
                MemberRank = "Bootlegger"
            };
            // We have created our expected gang member, I/O could still be occurring.
            // To block any further execution on the calling thread and force us to wait
            // until I/O finishes we call the .Result property (the same as the Wait method)
            // Note: We wouldn't want to call .Result before we instantiate the expected
            // member because this would defeat the purpose of doing async I/O.
            actualMember = actualMemberTask.Result;

            // Assert
            // Here we test that the expectedMember and actualMember match.
            Assert.AreEqual(expectedMember.MemberID, actualMember.MemberID);
            Assert.AreEqual(expectedMember.MemberName, actualMember.MemberName);
            Assert.AreEqual(expectedMember.MemberRank, actualMember.MemberRank);
        }

        [TestMethod]
        public void GetAllMembersSynchronouslyFromDbResultsMatchWithTestData()
        {
            // Arrange
            DataLayer dataLayer = new DataLayer();
            List<GangMember> actualMembers;
            List<GangMember> expectedMembers = new List<GangMember>();
            expectedMembers.Add(new GangMember { MemberID = 1, MemberName = "Gentleman Jack", MemberRank = "Bootlegger" });
            expectedMembers.Add(new GangMember { MemberID = 2, MemberName = "Brainy Somerville", MemberRank = "Head Honcho" });
            expectedMembers.Add(new GangMember { MemberID = 3, MemberName = "Bobby Tweaks", MemberRank = "Pickpocket" });
            expectedMembers.Add(new GangMember { MemberID = 4, MemberName = "Toothless Jim", MemberRank = "Brawler" });
            expectedMembers.Add(new GangMember { MemberID = 5, MemberName = "Whispering Sam", MemberRank = "Hitman" });

            // Act
            actualMembers = dataLayer.GetAllGangMembers();
            
            // Assert
            int incrementMember = 0;
            foreach (var gangMember in expectedMembers)
            {
                Assert.AreEqual(gangMember.MemberID, actualMembers[incrementMember].MemberID);
                Assert.AreEqual(gangMember.MemberName, actualMembers[incrementMember].MemberName);
                Assert.AreEqual(gangMember.MemberRank, actualMembers[incrementMember].MemberRank);
                incrementMember++;
            }
        }

        [TestMethod]
        public void GetAllMembersAsynchronouslyFromDbResultsMatchWithTestData()
        {
            // Arrange
            DataLayer dataLayer = new DataLayer();

            // Act
            // Create task that goes and does the I/O for us.
            Task<List<GangMember>> actualMembersTask = dataLayer.GetAllGangMembersAsync();

            // Arrange - This is arranging but in order to show the asyc behaviour, it has been implemented after the ACT
            // In the mean time we can instantiate our test data.
            List<GangMember> expectedMembers = new List<GangMember>();
            expectedMembers.Add(new GangMember { MemberID = 1, MemberName = "Gentleman Jack", MemberRank = "Bootlegger" });
            expectedMembers.Add(new GangMember { MemberID = 2, MemberName = "Brainy Somerville", MemberRank = "Head Honcho" });
            expectedMembers.Add(new GangMember { MemberID = 3, MemberName = "Bobby Tweaks", MemberRank = "Pickpocket" });
            expectedMembers.Add(new GangMember { MemberID = 4, MemberName = "Toothless Jim", MemberRank = "Brawler" });
            expectedMembers.Add(new GangMember { MemberID = 5, MemberName = "Whispering Sam", MemberRank = "Hitman" });
            
            // Act
            // With that instantiated we can wait for our results
            List<GangMember> actualMembers = actualMembersTask.Result;

            // Assert
            int incrementMember = 0;
            // Now we loop through the test data and check it against our results
            foreach (var gangMember in expectedMembers)
            {
                Assert.AreEqual(gangMember.MemberID, actualMembers[incrementMember].MemberID);
                Assert.AreEqual(gangMember.MemberName, actualMembers[incrementMember].MemberName);
                Assert.AreEqual(gangMember.MemberRank, actualMembers[incrementMember].MemberRank);
                incrementMember++;
            }
        }
    }
}
