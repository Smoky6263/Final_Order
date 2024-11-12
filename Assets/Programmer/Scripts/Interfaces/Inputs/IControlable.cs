public interface IControlable
{
    public void MoveInput(float x, float y);
    public void JumpIsPressed();
    public void JumpIsReleased();
    public void RollPressed();
}
