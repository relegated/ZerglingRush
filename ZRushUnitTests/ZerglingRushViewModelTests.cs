using System;
using System.Windows;
using NUnit.Framework;
using ZerglingRush.ViewModels;

namespace ZerglingRushViewModelTests
{
    public class Tests
    {
        private ZerglingRushViewModel viewModel;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CanCreateViewModel()
        {
            GivenAViewModel();

            Assert.That(viewModel, Is.Not.Null);
        }

        [Test]
        public void StartingGameInitializesCorrectly()
        {
            GivenTheGameIsStarted();

            Assert.That(viewModel.GameIsRunning, Is.True);
            ThenTheCommandCenterIsInitialized();
        }

        [Test]
        public void CanCreateZerglings()
        {
            GivenAViewModel();

            Assert.That(viewModel.Zerglings.Count, Is.EqualTo(0));

            viewModel.SpawnZergling();

            Assert.That(viewModel.Zerglings.Count, Is.EqualTo(1));
            Assert.That(viewModel.Zerglings[0].Health, Is.EqualTo(30));
            Assert.That(viewModel.Zerglings[0].Location, Is.EqualTo(new Point(335, 800)));
        }

        [TestCase(false, 335, 800)]
        [TestCase(false, 329, 50)]
        [TestCase(false, 471, 50)]
        [TestCase(false, 335, 44)]
        [TestCase(false, 335, 186)]
        [TestCase(true, 330, 50)]
        [TestCase(true, 470, 50)]
        [TestCase(true, 335, 45)]
        [TestCase(true, 335, 185)]
        public void IsZerglingCloseEnoughToAttack(bool isNextToCommandCenter, int x, int y)
        {
            GivenTheGameIsStarted();

            GivenAZergling(x, y);

            viewModel.AttackCommandCenter(viewModel.Zerglings[0]);

            var expectedCCHealth = isNextToCommandCenter ? 996 : 1000;

            Assert.That(viewModel.TheCommandCenter.Health, Is.EqualTo(expectedCCHealth));
        }

        [Test]
        public void TwoZerglingsOnlyOneInMeleeRange_OnlyOneAttacks()
        {
            GivenTheGameIsStarted();

            GivenAZergling(335, 800);
            GivenAZergling(330, 50);

            viewModel.Zerglings.ForEach(zergling => viewModel.AttackCommandCenter(zergling));

            Assert.That(viewModel.TheCommandCenter.Health, Is.EqualTo(996));
        }

        [Test]
        public void ZerglingTakesDamageOnlyWhenClicked()
        {
            GivenTheGameIsStarted();
            GivenAZergling(335, 800);

            WhenUserClicks(335, 800);
            WhenUserClicks(355, 820);
            WhenUserClicks(5, 800);
            WhenUserClicks(335, 7);

            Assert.That(viewModel.Zerglings[0].Health, Is.EqualTo(10));
        }

        private void WhenUserClicks(int x, int y)
        {
            viewModel.ProcessClickAt(x, y);
        }

        private void GivenTheGameIsStarted()
        {
            GivenAViewModel();
            viewModel.StartGame();
        }

        private void GivenAZergling(int x, int y)
        {
            viewModel.SpawnZergling();


            viewModel.Zerglings[0].Location = new Point()
            {
                X = x,
                Y = y
            };

        }

        private void GivenAViewModel()
        {
            viewModel = new ZerglingRushViewModel();
        }

        private void ThenTheCommandCenterIsInitialized()
        {
            Assert.That(viewModel.TheCommandCenter, Is.Not.Null);
            Assert.That(viewModel.TheCommandCenter.Health, Is.EqualTo(1000));
            Assert.That(viewModel.TheCommandCenter.Location, Is.EqualTo(new Point(335, 50)));
            Assert.That(viewModel.TheCommandCenter.Width, Is.EqualTo(130));
            Assert.That(viewModel.TheCommandCenter.Height, Is.EqualTo(130));
        }
    }
}