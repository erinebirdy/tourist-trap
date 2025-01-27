using Godot;

namespace Duality.states.player
{
    public class PlayerIdleState : BaseState<Player>
    {
        private Vector2? _throwTo = null;
        public override void OnEnter()
        {
            RefObj.BodySprite.Play("idle");
        }

        public override BaseState<Player> Update(float delta)
        {
            // calculate movement vector
            Vector2 movement = RefObj.GetMovementVector();

            if (_throwTo.HasValue && RefObj.HasFlag)
                return new PlayerThrowState(_throwTo.Value);
            else if (Input.IsActionJustPressed("interact"))
                return new PlayerInteractState();
            else if (Input.IsActionJustPressed("shout"))
                return new PlayerShoutState();
            else if (movement.LengthSquared() > 0)
                return new PlayerWalkState();
            
            return null;    
            
        }

        public override void OnLeftClick(Vector2 position)
        {
            _throwTo = position;
        }
    }
}