﻿//FOR EXTENDED INFO ABOUT METHODS AND VARIABLES CHECK SHIP CLASS

// Abilities:
// * high speed (both linear and rotational);     * below average health;      * average damage dealt modifier

using UnityEngine;
using System.Collections;

public class FastShip : Ship
{
    // Use this for initialization   

	public const float MAX_SPEED = 6f;
	public const float ROT_SPEED = 180f;   
	public const float DAMAGE_DEALT_MODIFIER = 0.5f;
    public const float HEALTH = 20f;

	private const float BOUNDARY_RADIUS = 0.5f;
    
    //component to call methods in Handlers
	private ShipLifeHandler _lifeHandler;
	private ShootHandler _shootHandler;

    
    override protected void Start()
    {
		_shootHandler = gameObject.GetComponentInChildren<ShootHandler>();
		_lifeHandler = gameObject.GetComponent<ShipLifeHandler>();

		MaxSpeed = MAX_SPEED; //speed we can reach in 1 sec
        RotSpeed = ROT_SPEED; //speed we can turn in 1 sec
        ShipBoundaryRadius = BOUNDARY_RADIUS;

		_shootHandler.DamageDealtModifier = DAMAGE_DEALT_MODIFIER;

		_shootHandler.PlayerLayer = this.gameObject.layer;

		NetworkServerUI.SendHealthInfo((gameObject.layer - Constants.LAYER_OFFSET) + 1, HEALTH, true);
		ResetHealth();      
    }

	// Update is called once per frame
		override protected void Update()
		{      
			//Movement();
		}
	 
	protected override void Movement()
	{ }
		/* Input.GetAxis is what we want to have 
        * so it's independant from both keyboard and joystick */


        //** ROTATE THE SHIP **//
/*        Quaternion rot = transform.rotation;
        float z = rot.eulerAngles.z;

        //minus to get the std rotaton (dxdx, sxsx)
        z -= Input.GetAxis("Horizontal") * RotSpeed * Time.deltaTime;
        rot = Quaternion.Euler(0, 0, z);
        transform.rotation = rot;
*/
        //** MOVE THE SHIP **//
/*        Vector3 pos = transform.position;

        // Input.GetAxis returns a FLOAT between -1.0 and 1.0 0 if not pressed
        Vector3 newPos = new Vector3(0, Input.GetAxis("Vertical") * MaxSpeed * Time.deltaTime, 0);

        pos += rot * newPos;

        //RESTRICT player to the cameras boundaries
        pos = this.BoundariesRestrictions(pos);

        transform.position = pos;
*/
		/* Also valid, unless u wanna do stuff in betweed :
         * transform.Translate( new Vector3(0, Input.GetAxis("Vertical") * maxSpeed * Time.deltaTime))*/
 /*   }     */

	override public void ResetHealth()
    {
        _lifeHandler.Health = HEALTH;
    }
   
	public override void Movement(float horizontalAxis, float verticalAxis)
	{
		//** ROTATE THE SHIP **//
		transform.eulerAngles = new Vector3(0, 0, -Mathf.Atan2(horizontalAxis, verticalAxis) * Mathf.Rad2Deg);
        
		//** MOVE THE SHIP **//

		Vector3 pos = transform.position;

        // Input.GetAxis returns a FLOAT between -1.0 and 1.0 0 if not pressed
		Vector3 newPos = new Vector3(horizontalAxis * MaxSpeed * Time.deltaTime,
		                             verticalAxis * MaxSpeed * Time.deltaTime, 0);
                                     
		pos += newPos;

		pos = this.BoundariesRestrictions(pos);


		transform.position = pos;
     
	}


}
