using UnityEngine;
using System.Collections;

public class EnemieController : MonoBehaviour {

/*====================== VARIABLES FOR CAR CONYTOLL ==========================*/
		public float speed;
    Rigidbody2D Body;
		public Rigidbody2D Motor;
		public Rigidbody2D Mover;
		public Animator Anim;
/*===================== VARIABLES FOR SMOOTHCAM SIZE =========================*/
		float DifSize;
		float SizerSpeed;
/*============================================================================*/

		void Start()
			{
				Body = GetComponent<Rigidbody2D>();
			}


		void Update() {
					float SpeedTyper = 1.1f;
					float vel = Body.velocity.magnitude;
					float MoverTyper = 15;


					Vector2 VectorVel = Body.velocity;
					float Direct = transform.InverseTransformDirection(VectorVel).y;
					if (Direct < 0) {Direct = -1;} else {Direct = 1;}
					//Debug.Log("На, сука: " + Direct);

/* ============================ AUTO MOTOR BOX ============================== */
					if (Input.GetKey(KeyCode.W)) {
						//Первая
						if (vel <= 1) {
						SpeedTyper = SpeedTyper * 1;
						Motor.AddRelativeForce(Vector2.up * speed * SpeedTyper);
						}
						//Вторая
						if (vel > 1 && vel <= 8) {
						SpeedTyper = SpeedTyper * 2.5f;
						Motor.AddRelativeForce(Vector2.up * speed * SpeedTyper);
						}
						//Третья
						if (vel > 8) {
						SpeedTyper = SpeedTyper * 5;
						Motor.AddRelativeForce(Vector2.up * speed * SpeedTyper);
						}
					}
/*=============================== BACK MOVE ================================= */
					if (Input.GetKey(KeyCode.S)) {
						//Тормоз на скорости
						if (vel > 15) {
						SpeedTyper = SpeedTyper * 5;
						Motor.AddRelativeForce(Vector2.down * speed * SpeedTyper);
						}
						//Тихий Задний Ход
						if (vel <= 15) {
						SpeedTyper = SpeedTyper * 2;
						Motor.AddRelativeForce(Vector2.down * speed * SpeedTyper);
						}
					}


/*============================ ЛЕВЫЙ ПОВОРОТНИК ============================= */
					if (Input.GetKey(KeyCode.A)) {
			  	Anim.CrossFade("Left", 0.3f);
							// На малых оборотах
						if (vel > 3 && vel <=15) {
						float MoverTyperI = MoverTyper * 10;
						Mover.AddRelativeForce(Vector2.left * vel * MoverTyperI * Direct);
						}
						// На скорости
						if (vel > 15) {
						float MoverTyperII = MoverTyper * 5;
						Mover.AddRelativeForce(Vector2.left * vel * MoverTyperII * Direct);
						}
					}
					if (Input.GetKeyUp(KeyCode.A)) {
			  	Anim.CrossFade("Idle", 0.3f);
					}


/*============================= ПРАВЫЙ ПОВОРОТНИК =========================== */
					if (Input.GetKey(KeyCode.D)) {
					Anim.CrossFade("Right", 0.3f);
						// На малых оборотах
						if (vel > 3 && vel <=15) {
						float MoverTyperI = MoverTyper * 10;
						Mover.AddRelativeForce(Vector2.right * vel * MoverTyperI * Direct);
						}
						// На скорости
						if (vel > 15) {
						float MoverTyperII = MoverTyper * 5;
						Mover.AddRelativeForce(Vector2.right * vel * MoverTyperII * Direct);
						}
					}
					if (Input.GetKeyUp(KeyCode.D)) {
					Anim.CrossFade("Idle", 0.3f);
					}
/*=============================== CAM SIZE ===================================*/
        DifSize = Mathf.SmoothDamp(DifSize, Body.velocity.magnitude * 0.5f, ref SizerSpeed, 1f);
				Camera.main.orthographicSize = 5.0f + DifSize;
/*============================================================================*/



			}
}
