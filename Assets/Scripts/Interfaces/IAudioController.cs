using Assets.Scripts.General;

namespace Assets.Scripts.Interfaces
{
    public interface IAudioController
    {
        void Play(GameSound gameSound);
        void Stop(GameSound gameSound);
    }
}
