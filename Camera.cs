using Godot;

namespace TerrariaClone
{
    public partial class Camera : Camera2D
    {
        private readonly float _speed = 1700f;
        private readonly Vector2 _minZoom = new(0.2f, 0.2f);
        private readonly Vector2 _maxZoom = new(2, 2);
        private readonly Vector2 _zoomSpeed = new(0.5f, 0.5f);

        public override void _Ready()
        {
            //Zoom = _minZoom;
            //Position = new(500, 1200);
        }

        public override void _Process(double delta)
        {
            if (Input.IsActionPressed("camera_zoom_in") && Zoom < _maxZoom)
            {
                Zoom += _zoomSpeed * (float)delta;
            }
            else if (Input.IsActionPressed("camera_zoom_out") && Zoom > _minZoom)
            {
                Zoom -= _zoomSpeed * (float)delta;
            }

            Vector2 movement = Vector2.Zero;

            if (Input.IsActionPressed("camera_left"))
                movement.X--;
            if (Input.IsActionPressed("camera_right"))
                movement.X++;
            if (Input.IsActionPressed("camera_up"))
                movement.Y--;
            if (Input.IsActionPressed("camera_down"))
                movement.Y++;

            if (movement != Vector2.Zero)
            {
                Position += movement.Normalized() * _speed * (float)delta;
            }
        }
    }
}