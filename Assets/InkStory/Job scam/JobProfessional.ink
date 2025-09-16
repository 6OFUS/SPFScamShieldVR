-> open_notification
=== open_notification ===
+ [Player:action_tap_notification Tap the notification] -> job_offer_intro

//------------------------------ JOB OFFER INTRO ------------------------------
=== job_offer_intro ===
Hello Taylor, this is Lucia from the Talent Acquisition team at T Commerce.<br><br>We've reviewed your application for the <b>Customer Support & Operations</b> role and would love to connect to learn more about you and your experience.<br><br>T-Commerce is a digital solutions company focused on building smart, user-centric platforms for businesses across Southeast Asia.<br><br>Would you be available for a short WhatsUp call today? Just about 10 minutes. #Sender:message

+ [Player:message Is this job legit?] -> job_offer_dialogue_1
+ [Player:message Can I ask more about the role first before the call?] -> job_offer_dialogue_2
+ [Player:message Scammer lah you!] -> send_sticker

=== send_sticker === 
+ [Player:sticker Sticker 1] -> job_offer_dialogue_3
+ [Player:sticker Sticker 2] -> job_offer_dialogue_3
+ [Player:sticker Sticker 3] -> job_offer_dialogue_3

=== job_offer_dialogue_1 ===
Totally understand the concern. Yes, this is a legitimate role.<br><br>You applied via Linkedin, and I'm reaching out on behalf of T Commerce's HR team.<br><br>You can also check us out at:<br><color=blue><u>www.t-commerce.com</u></color> #Sender:message

+ [Player:action_check_website Check the site] -> return_to_chat

=== return_to_chat ===
+ [Player:action_return_to_chat Return to WhatsUp chat] -> job_verification_dialogue_1 

=== job_offer_dialogue_2 ===
The Customer Support & Operations Executive role is full-time and mostly remote.<br><br>You'll be handling basic customer enquiries, managing orders, and assisting with operational tasks on platforms like Shopee and Lazada.<br><br>Training will be provided, and we're a small but friendly team! #Sender:message

+ [Player:win_ending Sounds good! I'm okay to take the call today.] -> END
+ [Player:message Is this job legit?] -> job_offer_dialogue_1

=== job_offer_dialogue_3 ===
Hey Taylor... not sure if that was meant as a joke, but just to clarify, I'm a legitimate recruiter from T-Commerce Asia.<br><br>We reached out because you applied on LinkedIn, and we really liked your profile.<br><br>It's okay to double-check things but calling someone a scammer without confirming first isn't very respectful.<br><br>I'll withdraw your application for now. Best of luck with your job search. #Sender:message

+ [Player:lose_ending Ok, I'm really sorry.]-> END

=== job_verification_dialogue_1 ===
//OPEN WEBSITE THEN REPLY
+ [Player:win_ending Just checked the site. Looks okay, let's proceed.] -> win_ending
+ [Player:message Found your site but can't find your name there. Are you really part of HR?] -> job_verification_dialogue_2

=== job_verification_dialogue_2 === 
No worries at all! My email is listed on the Careers page, easy to miss sometimes.<br><br>I'm with the onboarding team under HR and totally understand the need to double check.<br><br>Let me know if you'd like me to loop in a colleague or resend a formal email for peace of mind! #Sender:message

+ [Player:message Appreciate the clarification. Could you send a formal intro email?] -> job_verification_email_dialogue_1

=== job_verification_email_dialogue_1 ===
Alright email sent! #Sender:message
//OPEN EMAIL 

+ [Player:action_open_amail Check Amail] -> return_from_email

=== return_from_email ===
+ [Player:action_return_to_chat Open WhatsUp] -> win_ending

=== win_ending ===
+ [Player:win_ending Thank you. Let's proceed.] -> END

