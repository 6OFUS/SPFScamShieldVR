-> job_offer_intro

//------------------------------ JOB OFFER INTRO ------------------------------
=== job_offer_intro ===
hi feiyang
this is lucia from t commerce asia
you applied for a positioj
lets schedule interview

//PLAYER CHOICES
+ [Player:message Is this job legit?] -> job_offer_dialogue_1
+ [Player:message Scammer lah you!] -> send_sticker
+ [Player:submit_scamshield Screenshot and submit to ScamShield] -> report_scamshield

=== send_sticker === 
+ [Player: Sticker 1] -> job_offer_dialogue_3
+ [Player: Sticker 2] -> job_offer_dialogue_3
+ [Player: Sticker 3] -> job_offer_dialogue_3

=== job_offer_dialogue_1 ===
hey feiyang, no worries at all 
yup this is a legit opening 
you applied through linkedin for the e-commerce support role, right?
i'm from T Commerce Asia's hiring team! you can check us out at <color=blue><u>www.t-commerce.com</u></color>
my contact email is lucia.tan@t-commerce.com
let me know if you're keen to proceed, happy to share more!

//PLAYER CHOICES
+ [Player: Tap on the link] -> tap_on_website_link
+ [Player:message Hmm... I still don't really believe you leh.] -> job_offer_dialogue_2
+ [Player:submit_scamshield Screenshot and submit to ScamShield] -> report_scamshield

=== job_offer_dialogue_2 ===
totally understand - it's smart to be careful!
if it helps, you can reply to the email i sent from lucia.tan@t-commerce.com, or even call our office line listed on the website to confirm my name.
no pressure at all - just let me know if you'd like to move forward or need more info first 

+ [Player:message Just checked the site. Looks okay, let's proceed.] -> win_ending
+ [Player:submit_scamshield Screenshot and submit to ScamShield] -> report_scamshield


=== job_offer_dialogue_3
hey feiyang... not sure if that was meant as a joke, but just to clarify  i'm a legit recruiter from T-Commerce Asia.
we reached out because you applied on LinkedIn, and we really liked your profile.
it's okay to double-check things - but calling someone a scammer without confirming first isn't very respectful.
i'll withdraw your application for now. best of luck with your job search.

-> lose_ending

=== tap_on_website_link === 
//SHOW COMPANY WEBSITE HERE
-> return_from_website

=== return_from_website ===
//BACK BUTTON TO CONTINUE STORY
+ [Player:message Just checked the site. looks okay, lets proceed.] -> win_ending
+ [Player:message Found your site but can't find your name there. are you really part of HR?] -> job_verification_dialogue_1
+ [Player:submit_scamshield Screenshot and submit to ScamShield] -> report_scamshield

//------------------------------ JOB VERIFICATION ------------------------------
=== job_verification_dialogue_1 ===
haha no worries! my email's actually listed under the Contact Us section - might be easy to miss
but yup, i'm part of the onboarding team under HR. totally understand the need to double check 
let me know if you'd like me to loop in another colleague or send a formal email again for peace of mind

+ [Player:message Appreciate the clarification. Could you send a formal intro email again? Just to be safe.] -> job_verification_dialogue_2
+ [Player:submit_scamshield Screenshot and submit to ScamShield] -> report_scamshield

=== job_verification_dialogue_2 === 
//GO TO PHONE HOME PAGE -> OPEN EMAIL -> OPEN MAIL SENT BY RECRUITER
-> return_from_email

=== return_from_email ===
+ [Player:message Thank you! Let's proceed.] -> win_ending
+ [Player: Ignore recruiter] -> lose_not_scam_ending
+ [Player:submit_scamshield Screenshot and submit to ScamShield] -> report_scamshield

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

