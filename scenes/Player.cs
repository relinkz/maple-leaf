using Godot;


public partial class Player : CharacterBody2D
{
	private AnimationPlayer _animationPlayer;

	[Export]
	public double Speed { get; set; } = 400;
	private bool _isMoving = false;
	private Vector2 _direction = Vector2.Down;

	public void _ready()
	{
		_animationPlayer = GetChild<AnimationPlayer>(2);

		_animationPlayer.Play("idle_down");
	}

	public override void _Input(InputEvent @event)
	{
		shouldPlayIdle(@event);
	}

	private void updateDirection()
	{
		if (isPlayerMoving())
		{
			Vector2 dir = Vector2.Zero;
			if (Input.IsActionPressed("left"))
			{
				dir += Vector2.Left;
			}
			if (Input.IsActionPressed("right"))
			{
				dir += Vector2.Right;
			}
			if (Input.IsActionPressed("up"))
			{
				dir += Vector2.Up;
			}
			if (Input.IsActionPressed("down"))
			{
				dir += Vector2.Down;
			}

			_direction = dir;
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		if (isPlayerMoving())
		{
			updateDirection();
			playWalkAnimationBasedOnDirection();

			// Vector2 speed = _direction.Normalized() * (float)Speed;
			Velocity = _direction * (float)60;

		}
		else
		{
			Velocity = Vector2.Zero;
		}

		MoveAndSlide();
	}

	public override void _Process(double delta)
	{
	}

	private void shouldPlayIdle(InputEvent @event)
	{
		if (isPlayerMoving())
		{
			_isMoving = true;
			Velocity = Vector2.Zero;
			return;
		}
		if (@event.IsActionReleased("left"))
		{
			_animationPlayer.Play("idle_left");
		}
		else if (@event.IsActionReleased("right"))
		{
			_animationPlayer.Play("idle_right");
		}
		else if (@event.IsActionReleased("up"))
		{
			_animationPlayer.Play("idle_up");
		}
		else if (@event.IsActionReleased("down"))
		{
			_animationPlayer.Play("idle_down");
		}

		_isMoving = false;
	}

	private void playWalkAnimationBasedOnDirection()
	{
		if (_direction.X < 0.0f)
		{
			_animationPlayer.Play("walk_left");
			return;
		}
		else if (_direction.X > 0.0f)
		{
			_animationPlayer.Play("walk_right");
			return;
		}
		if (_direction.Y > 0.0f)
		{
			_animationPlayer.Play("walk_down");
		}
		if (_direction.Y < 0.0f)
		{
			_animationPlayer.Play("walk_up");
		}
	}

	private bool isPlayerMoving()
	{
		return Input.IsActionPressed("left") || Input.IsActionPressed("right") || Input.IsActionPressed("up") || Input.IsActionPressed("down");
	}
}
