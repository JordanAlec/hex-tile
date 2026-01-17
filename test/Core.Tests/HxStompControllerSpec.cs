using Core.Interfaces;
using Core.Models.Requests;
using Core.Models.Responses;
using Core.Tests.Setup;
using NAudio.Midi;
using NSubstitute;

namespace Core.Tests
{
    public class HxStompControllerSpec
    {
        private readonly IMidiDeviceService _midiService;
        private readonly HxStompController _controller;

        public HxStompControllerSpec()
        {
            _midiService = Substitute.For<IMidiDeviceService>();
            _midiService.SetupFind();
            _midiService.SetupSuccessfulSendMidiCommand();
            _controller = new HxStompController(_midiService);
        }

        [Fact]
        public void ToggleTuner_Success()
        {
            var expectedResult = new SendMidiCommandResponse(
                new SendMidiCommandRequest(68, 127, 1),
                true
            );

            var result = _controller.ToggleTuner();
            AssertSuccessfulResult(expectedResult, result);
        }

        [Fact]
        public void PreviousPreset_Success()
        {
            var expectedResult = new SendMidiCommandResponse(
                new SendMidiCommandRequest(72, 0, 1),
                true
            );

            var result = _controller.PreviousPreset();
            AssertSuccessfulResult(expectedResult, result);
        }

        [Fact]
        public void NextPreset_Success()
        {
            var expectedResult = new SendMidiCommandResponse(
                new SendMidiCommandRequest(72, 127, 1),
                true
            );

            var result = _controller.NextPreset();
            AssertSuccessfulResult(expectedResult, result);
        }

        [Fact]
        public void FS1_Success()
        {
            var expectedResult = new SendMidiCommandResponse(
                new SendMidiCommandRequest(49, 0, 1),
                true
            );

            var result = _controller.FS1();
            AssertSuccessfulResult(expectedResult, result);
        }

        [Fact]
        public void FS2_Success()
        {
            var expectedResult = new SendMidiCommandResponse(
                new SendMidiCommandRequest(50, 0, 1),
                true
            );

            var result = _controller.FS2();
            AssertSuccessfulResult(expectedResult, result);
        }

        [Fact]
        public void FS3_Success()
        {
            var expectedResult = new SendMidiCommandResponse(
                new SendMidiCommandRequest(51, 0, 1),
                true
            );

            var result = _controller.FS3();
            AssertSuccessfulResult(expectedResult, result);
        }

        [Fact]
        public void FS4_Success()
        {
            var expectedResult = new SendMidiCommandResponse(
                new SendMidiCommandRequest(52, 0, 1),
                true
            );

            var result = _controller.FS4();
            AssertSuccessfulResult(expectedResult, result);
        }

        [Fact]
        public void FS5_Success()
        {
            var expectedResult = new SendMidiCommandResponse(
                new SendMidiCommandRequest(53, 0, 1),
                true
            );

            var result = _controller.FS5();
            AssertSuccessfulResult(expectedResult, result);
        }

        [Fact]
        public void FS6_Success()
        {
            var expectedResult = new SendMidiCommandResponse(
                new SendMidiCommandRequest(54, 0, 1),
                true
            );

            var result = _controller.FS6();
            AssertSuccessfulResult(expectedResult, result);
        }

        [Fact]
        public void FS7_Success()
        {
            var expectedResult = new SendMidiCommandResponse(
                new SendMidiCommandRequest(55, 0, 1),
                true
            );

            var result = _controller.FS7();
            AssertSuccessfulResult(expectedResult, result);
        }

        [Fact]
        public void FS8_Success()
        {
            var expectedResult = new SendMidiCommandResponse(
                new SendMidiCommandRequest(56, 0, 1),
                true
            );

            var result = _controller.FS8();
            AssertSuccessfulResult(expectedResult, result);
        }

        [Fact]
        public void Snapshot1_Success()
        {
            var expectedResult = new SendMidiCommandResponse(
                new SendMidiCommandRequest(69, 0, 1),
                true
            );

            var result = _controller.Snapshot1();
            AssertSuccessfulResult(expectedResult, result);
        }

        [Fact]
        public void Snapshot2_Success()
        {
            var expectedResult = new SendMidiCommandResponse(
                new SendMidiCommandRequest(69, 1, 1),
                true
            );

            var result = _controller.Snapshot2();
            AssertSuccessfulResult(expectedResult, result);
        }

        [Fact]
        public void Snapshot3_Success()
        {
            var expectedResult = new SendMidiCommandResponse(
                new SendMidiCommandRequest(69, 2, 1),
                true
            );

            var result = _controller.Snapshot3();
            AssertSuccessfulResult(expectedResult, result);
        }

        [Fact]
        public void Snapshot4_Success()
        {
            var expectedResult = new SendMidiCommandResponse(
                new SendMidiCommandRequest(69, 3, 1),
                true
            );

            var result = _controller.Snapshot4();
            AssertSuccessfulResult(expectedResult, result);
        }

        [Fact]
        public void NextSnapshot_Success()
        {
            var expectedResult = new SendMidiCommandResponse(
                new SendMidiCommandRequest(69, 8, 1),
                true
            );

            var result = _controller.NextSnapshot();
            AssertSuccessfulResult(expectedResult, result);
        }

        [Fact]
        public void PreviousSnapshot_Success()
        {
            var expectedResult = new SendMidiCommandResponse(
                new SendMidiCommandRequest(69, 9, 1),
                true
            );

            var result = _controller.PreviousSnapshot();
            AssertSuccessfulResult(expectedResult, result);
        }

        private void AssertSuccessfulResult(SendMidiCommandResponse expectedResult, SendMidiCommandResponse actualResult)
        {
            Assert.Equivalent(expectedResult, actualResult);

            _midiService.Received().Find("HX Stomp");
            _midiService.Received().SendMidiCommand(Arg.Any<MidiOut>(),
                Arg.Is<SendMidiCommandRequest>(r => r == expectedResult.Request)
            );
        }
    }
}