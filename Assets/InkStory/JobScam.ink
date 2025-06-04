=== start ===
Pick up the phone 

//run this after picking up phone
+ [Phone picked up] -> after_pickup

=== after_pickup ===
What are you going to do next?
+ [Click on the link] -> click_link
+ [Report to ScamShield] -> report_scamshield
+ [Check the source] -> check_source

=== click_link ===
Clicked on link
-> END

=== report_scamshield ===
Take a screenshot of the message
Open the ScamShield app
-> END

=== check_source === 
Checking source
-> END


