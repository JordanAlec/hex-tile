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