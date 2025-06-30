-> job_offer_intro

//------------------------------ JOB OFFER INTRO ------------------------------
=== job_offer_intro ===
Hello Feiyang, this is Lucia from the Talent Acquisition team at T Commerce.<br><br>We've reviewed your application for the <b>Customer Support & Operations</b> role and would love to connect to learn more about you and your experience.<br><br>T-Commerce is a digital solutions company focused on building smart, user-centric platforms for businesses across Southeast Asia.<br><br>Would you be available for a short WhatsApp call today? Just about 10 minutes #Sender:message

+ [Player:message Is this job legit?] -> job_offer_dialogue_1
+ [Player:message Can I ask more about the role first before the call?] -> job_offer_dialogue_2
+ [Player:message Scammer lah you!] -> send_sticker

=== send_sticker === 
+ [Player:sticker Sticker 1] -> job_offer_dialogue_3
+ [Player:sticker Sticker 2] -> job_offer_dialogue_3
+ [Player:sticker Sticker 3] -> job_offer_dialogue_3

=== job_offer_dialogue_1 ===
Totally understand the concern. Yes, this is a legitimate role.<br><br>You applied via Linkedin, and I'm reaching out on behalf of T Commerce's HR team.<br><br>You can also check us out at:<br><color=blue><u>www.t-commerce.com</u></color> #Sender:message

+ [Player:check_website Check the site] -> END //job_verification_dialogue_1
//+ [Player:submit_scamshield Screenshot and submit to Scamshield] -> report_scamshield

=== job_offer_dialogue_2 ===
The Customer Support & Operations Executive role is full-time and mostly remote.<br><br>You'll be handling basic customer enquiries, managing orders, and assisting with operational tasks on platforms like Shopee and Lazada.<br><br>Training will be provided, and we're a small but friendly team! 

+ [Player:message Sounds good! I'm okay to take the call today.] -> win_ending
+ [Player:message Is this job legit?] -> job_offer_dialogue_1
//+ [Player:submit_scamshield Screenshot and submit to Scamshield] -> report_scamshield

=== job_offer_dialogue_3 ===
hey feiyang... not sure if that was meant as a joke, but just to clarify, I'm a legit recruiter from T-Commerce Asia.<br><br>We reached out because you applied on LinkedIn, and we really liked your profile.<br><br>It's okay to double-check things but calling someone a scammer without confirming first isn't very respectful.<br><br>I'll withdraw your application for now. best of luck with your job search. #Sender:message
-> lose_ending

=== job_verification_dialogue_1 ===
//OPEN WEBSITE THEN REPLY
+ [Player:message Just checked the site. Looks okay, let's proceed] -> win_ending
+ [Player:message Found your site but can't find your name there. Are you really part of HR?] -> job_verification_dialogue_2
//+ [Player:submit_scamshield Screenshot and submit to Scamshield] -> report_scamshield

=== job_verification_dialogue_2 === 
No worries at all! My email is listed on the Careers page, easy to miss sometimes.<br><br>I'm with the onboarding team under HR and totally understand the need to double check.<br><br>Let me know if you'd like me to loop in a colleague or resend a formal email for peace of mind! #Sender:message

+ [Player:message Appreciate the clarification. Could you send a formal intro email again?] -> job_verification_email_dialogue_1
+ [Player:submit_scamshield Screenshot and submit to Scamshield] -> report_scamshield

=== job_verification_email_dialogue_1 ===
Alright email sent! #Sender:message
//OPEN EMAIL 

+ [Player:open_amail Check Amail] -> win_ending

+ [Player:message Thank you. Let's proceed.] -> win_ending
//+ [Player:submit_scamshield Screenshot and submit to Scamshield] -> report_scamshield

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