-> job_offer_intro

//------------------------------ JOB OFFER INTRO ------------------------------
=== job_offer_intro ===
Hello Feiyang, this is Lucia from the Talent Acquisition team at NovaLink Solutions. <br><br>We've reviewed your application for the E-Commerce Support Executive role and would love to connect to learn more about you and your experience. <br><br>NovaLink is a digital solutions company focused on building smart, user-centric platforms for businesses across Southeast Asia. <br><br>Would you be available for a short WhatsApp call today? Just about 10 minutes.

+ [Is this job legit?] -> job_offer_dialogue_1
+ [Thanks for the message. Can I ask more about the role first before the call?] -> job_offer_dialogue_2
+ [Scammer lah you!] -> send_sticker

=== send_sticker === 
+ [Sticker 1] -> job_offer_dialogue_3
+ [Sticker 2] -> job_offer_dialogue_3
+ [Sticker 3] -> job_offer_dialogue_3

=== job_offer_dialogue_1 ===
Totally understand the concern. Yes, this is a legitimate role. You applied via Linkedin, and I'm reaching out on behalf of NovaLink's HR team. <br><br>You can also check us out at: <color=blue><u>www.novalinksolutions.com</u></color>

+ [Tap on link] -> job_verification_dialogue_1
+ [Screenshot and submit to Scamshield] -> report_scamshield

=== job_offer_dialogue_2 ===
The E-Commerce Support Executive role is full-time and mostly remote. You'll be helping with product listing updates, basic customer enquiries, and managing orders on platforms like Shopee and Lazada. Training is provided, and we have a small but friendly team! Let me know if you'd like to proceed to a call and I'll slot you in. 

+ [Sounds good! I'm okay to take the call today.] -> win_ending
+ [Screenshot and submit to Scamshield] -> report_scamshield

=== job_offer_dialogue_3 ===
Hey feiyang... not sure if that was meant as a joke, but just to clarifym I'm a legit recruiter from T-Commerce Asia. We reached out because you applied on LinkedIn, and we really liked your profile. It's okay to double-check things but calling someone a scammer without confirming first isn't very respectful. I'll withdraw your application for now. Best of luck with your job search.
-> lose_ending

=== job_verification_dialogue_1 ===
//OPEN WEBSITE THEN REPLY
+ [Just checked the site. Looks okay, lets proceed] -> win_ending
+ [Found your site but can't find your name there. Are you really part of HR?] -> job_verification_dialogue_2
+ [Screenshot and submit to Scamshield] -> report_scamshield

=== job_verification_dialogue_2 === 
Haha no worries! My email's actually listed under the Contact Us section, might be easy to miss. But yup, I'm part of the onboarding team under HR. Totally understand the need to double check. Let me know if you'd like me to loop in another colleague or send a formal email again for peace of mind

+ [Appreciate the clarification. Could you send a formal intro email again? Just to be safe.] -> job_verification_email_dialogue_1
+ [Screenshot and submit to Scamshield] -> report_scamshield

=== job_verification_email_dialogue_1 ===
//OPEN EMAIL 
+ [Thank you. Let's proceed.] -> win_ending
+ [Ignore the job offer] -> lose_ending
+ [Screenshot and submit to Scamshield] -> report_scamshield

//------------------------------ SCAMSHIELD PROCEDURE ------------------------------
=== report_scamshield ===
//PLAYER GO THROUGH FULL SCAMSHIELD PROCEDURE
-> lose_not_scam_ending

//------------------------------ ENDINGS ------------------------------
=== win_ending ===
//PLAYER WINS
-> END

=== lose_not_scam_ending ===
-> END

=== lose_ending ===
-> END