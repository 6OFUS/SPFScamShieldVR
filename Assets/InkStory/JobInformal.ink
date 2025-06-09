-> job_offer_intro

//------------------------------ JOB OFFER INTRO ------------------------------
=== job_offer_intro ===
hi feiyang
this is lucia from t commerce asia
you applied for a positioj
lets schedule interview

//PLAYER CHOICES
+ [Is this job legit?] -> job_offer_dialogue_1
+ [Scammer lah you!] -> send_sticker
+ [Screenshot and submit to ScamShield] -> report_scamshield

=== report_scamshield ===
//PLAYER GO THROUGH FULL SCAMSHIELD PROCEDURE
//AT END LOSE
Reporting
-> lose_ending

=== job_offer_dialogue_1 ===
hey feiyang, no worries at all 
yup this is a legit opening 
you applied through linkedin for the e-commerce support role, right?
i'm from T Commerce Asia's hiring team! you can check us out at <color=blue><u>www.t-commerce.com</u></color>
my contact email is lucia.tan@t-commerce.com
let me know if you're keen to proceed, happy to share more!

//PLAYER CHOICES
+ [Tap on the link] -> tap_on_website_link
+ [hmm... i still don't really believe you leh] -> job_offer_dialogue_2
+ [Screenshot and submit to ScamShield] -> report_scamshield

=== job_offer_dialogue_2 ===
totally understand - it's smart to be careful!
if it helps, you can reply to the email i sent from lucia.tan@t-commerce.com, or even call our office line listed on the website to confirm my name.
no pressure at all - just let me know if you'd like to move forward or need more info first 

+ [just checked the site. looks okay, lets proceed] -> win_ending
+ [Screenshot and submit to ScamShield] -> report_scamshield

=== send_sticker === 
+ [Sticker 1] -> job_offer_dialogue_3
+ [Sticker 2] -> job_offer_dialogue_3
+ [Sticker 3] -> job_offer_dialogue_3

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
+ [just checked the site. looks okay, lets proceed] -> win_ending
+ [found your site but can't find your name there. are you really part of HR?] -> job_verification_dialogue_1
+ [Screenshot and submit to ScamShield] -> report_scamshield

=== job_verification_dialogue_1 ===
haha no worries! my email's actually listed under the Contact Us section - might be easy to miss
but yup, i'm part of the onboarding team under HR. totally understand the need to double check 
let me know if you'd like me to loop in another colleague or send a formal email again for peace of mind

+ [appreciate the clarification. could you send a formal intro email again? just to be safe.] -> job_verification_dialogue_2
+ [Screenshot and submit to ScamShield] -> report_scamshield

=== job_verification_dialogue_2 === 
//GO TO PHONE HOME PAGE -> OPEN EMAIL -> OPEN MAIL SENT BY RECRUITER
-> return_from_email

=== return_from_email ===
+ [thank you! let's proceed] -> win_ending
+ [Ignore recruiter] -> lose_ending
+ [Screenshot and submit to ScamShield] -> report_scamshield

=== win_ending ===
//PLAYER WINS
player wins
-> END

=== lose_ending ===
//PLAYER LOSES
player loses
-> END