-> job_offer_intro

//------------------------------ JOB OFFER INTRO ------------------------------
=== job_offer_intro ===
hi feiyang #Sender:message
this is lucia from t commerce asia #Sender:message
you applied for a positioj #Sender:message
lets schedule interview #Sender:message

//PLAYER CHOICES
+ [Player:message Is this job legit?] -> job_offer_dialogue_1
+ [Player:message Scammer lah you!] -> send_sticker
//+ [Player:submit_scamshield Screenshot and submit to ScamShield] -> report_scamshield

=== send_sticker === 
+ [Player:sticker Sticker 1] -> job_offer_dialogue_3
+ [Player:sticker Sticker 2] -> job_offer_dialogue_3
+ [Player:sticker Sticker 3] -> job_offer_dialogue_3

=== job_offer_dialogue_1 ===
hey feiyang, no worries at all #Sender:message
yup this is a legit opening <br>you applied through linkedin for the e-commerce support role, right? #Sender:message
i'm from T Commerce Asia's hiring team! you can check us out at [<color=blue><u>www.t-commerce.com</u></color>] #Sender:message
my contact email is lucia.tan@t-commerce.com #Sender:message
let me know if you're keen to proceed, happy to share more! #Sender:message

//PLAYER CHOICES
+ [Player:check_website Check the site] -> tap_on_website_link
+ [Player:message Hmm... I still don't really believe you leh.] -> job_offer_dialogue_2
//+ [Player:submit_scamshield Screenshot and submit to ScamShield] -> report_scamshield

=== job_offer_dialogue_2 ===
Hey no worries at all, it's smart to be cautious! #Sender:message
No pressure, just let me know if you'd like to go ahead or if you need more info first! #Sender:message

+ [Player:check_website Check the site] -> tap_on_website_link
//+ [Player:submit_scamshield Screenshot and submit to ScamShield] -> report_scamshield


=== job_offer_dialogue_3 ===
hey feiyang... not sure if that was meant as a joke, but just to clarify i'm a legit recruiter from T-Commerce Asia.<br>we reached out because you applied on LinkedIn, and we really liked your profile.<br>it's okay to double-check things - but calling someone a scammer without confirming first isn't very respectful.<br>i'll withdraw your application for now. best of luck with your job search. #Sender:message

+ [Player:jobless_lose_ending Ok, I'm really sorry.]-> lose_ending

=== tap_on_website_link === 
//SHOW COMPANY WEBSITE HERE
-> END

=== return_from_website ===
//BACK BUTTON TO CONTINUE STORY
+ [Player:win_ending Just checked the site. Looks okay, lets proceed.] -> END
+ [Player:message Found your site but can't find your name there. Are you really part of HR?] -> job_verification_dialogue_1
//+ [Player:submit_scamshield Screenshot and submit to ScamShield] -> report_scamshield

//------------------------------ JOB VERIFICATION ------------------------------
=== job_verification_dialogue_1 ===
haha no worries! my email's actually listed under the Contact Us section - might be easy to miss #Sender:message
but yup, i'm part of the onboarding team under HR. totally understand the need to double check #Sender:message
let me know if you'd like me to loop in another colleague or send a formal email again for peace of mind #Sender:message

+ [Player:message Appreciate the clarification. Could you send a formal intro email again? Just to be safe.] -> job_verification_dialogue_2
//+ [Player:submit_scamshield Screenshot and submit to ScamShield] -> report_scamshield

=== job_verification_dialogue_2 === 
Alright email sent! #Sender:message

//GO TO PHONE HOME PAGE -> OPEN EMAIL -> OPEN MAIL SENT BY RECRUITER
+ [Player:open_amail Check Amail] -> END
//-> return_from_email

=== return_from_email ===
+ [Player:win_ending Thank you! Let's proceed.] -> END
//+ [Player:submit_scamshield Screenshot and submit to ScamShield] -> report_scamshield

//------------------------------ SCAMSHIELD PROCEDURE ------------------------------
=== report_scamshield ===
//PLAYER GO THROUGH FULL SCAMSHIELD PROCEDURE
-> lose_not_scam_ending

//------------------------------ ENDINGS ------------------------------
=== win_ending ===
//PLAYER WINS
-> END

=== lose_ending ===
-> END

=== lose_not_scam_ending ===
-> END

