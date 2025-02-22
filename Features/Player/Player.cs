using Godot;

namespace ProceduralGeneration.Features.Player
{
    public partial class Player : CharacterBody2D
    {
        public const float Speed = 300.0f;
        public const float JumpVelocity = -400.0f;

        public bool CanMove { get; set; } = false;

        public override void _PhysicsProcess(double delta)
        {
            if (Input.IsActionJustReleased("spawn_player"))
                CanMove = !CanMove;

            if (!CanMove)
                return;

            Vector2 velocity = Velocity;

            // Add the gravity.
            if (!IsOnFloor())
            {
                velocity += GetGravity() * (float)delta;
            }

            // Handle Jump.
            if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
            {
                velocity.Y = JumpVelocity;
            }

            // Get the input direction and handle the movement/deceleration.
            // As good practice, you should replace UI actions with custom gameplay actions.
            Vector2 direction = Input.GetVector("move_left", "move_right", "move_up", "move_down");

            if (direction != Vector2.Zero)
            {
                velocity.X = direction.X * Speed;
            }
            else
            {
                velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
            }

            Velocity = velocity;
            MoveAndSlide();
        }
    }
}