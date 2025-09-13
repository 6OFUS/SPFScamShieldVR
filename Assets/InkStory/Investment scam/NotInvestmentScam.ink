-> open_notification

=== open_notification ===
+ [Player:action_tap_notification Tap on the notification] -> dialogue_1

=== dialogue_1 ===
Hey Taylor! Long time no talk. How've you been? #Sender:message

+ [Player:message Hey Rachel! Yeah, it's been a while. I'm doing good, you?] -> dialogue_2
+ [Player:message Haha wow! Feels strange seeing your name pop up. I'm doing good!] -> dialogue_2

=== dialogue_2 ===
Hehe, glad to hear that you are doing good! I'm now a certified financial advisor. Oya let me know if u wanna start investing! #Sender:message

+ [Player:message_add_pfp Real or not?] -> dialogue_3
+ [Player:message_add_pfp Hmm... I'm actually interested but kinda scared to start.] -> dialogue_4

=== dialogue_3 ===
LOL, Taylor, don't believe you can go check lor. I've been investing and making some good money. You should try it too! #Sender:message

+ [Player:message Haha, why you want me to try sia. sounds sus.] -> dialogue_5

=== dialogue_4 ===
Oh I can help. Don't worry,want me to walk you through it? Just incase you think im a scam... My company provide regulated investment products like fixed income funds and ETFs. But first, I want to assure you this is 100% legit and regulated by MAS. #Sender:message

+ [Player:message Uhh... maybe a call? I'm kinda slow with this stuff.] -> dialogue_6
+ [Player:message How do I know you're legit?] -> dialogue_7

=== dialogue_5 ===
Haha I just saw your story and found out you're the founder of BeiBei! Wah this is big eh I can't believe you never shared it with me. Anyways, I was thinking... if you ever need any financial advice to help you plan better or manage your money more efficiently, just let me know! #Sender:message

+ [Player:message oh i actually need help tho. How do I know you're not trying to scam me?] -> dialogue_7

=== dialogue_6 ===
I can't call now, I'm outside :( you can check whether im legitimate online.<br>- Go to the MAS Register of Representatives.<br>- Search for Rachel under Capital Partners Company Singapore (PTE) Limited.<br>- My Representative Number is R123456Z you'll see my official license listed there.<br>- You can also call our official hotline at 6812 3456 to double-check.<br><br>No force ah just let me know if u want to proceed with me. I'm glad to help you. Just like how u helped me in class last time :) #Sender:message

+[Player:message Wow, you are under Capital Partners! I remember you needed help for math last time. now already a successful advisor working at Capital Partner.] -> dialogue_8

=== dialogue_7 === 
Great question here's how you can verify me:<br> 1. Go to the MAS Register of Representatives.<br> 2. Search for Rachel under Capital Partners Company Singapore (PTE) Limited.<br> 3. My Representative Number is R123456Z you'll see my official license listed there.<br> 4. You can also call our official hotline at 6812 3456 to double-check.<br><br>No force ah just let me know if u want to proceed with me. I'm glad to help you. Just like how u helped me in class last time :) #Sender:message

+[Player:message Wow, you are under Capital Partners! I remember you needed help for math last time. now already a successful advisor working at Capital Partner.] -> dialogue_8

=== dialogue_8 ===
Hahaha Thanks! #Sender:message
+[Player:message Wait ah let me go check first.] ->  dialogue_9

=== dialogue_9 ===
Okie! #Sender:message
+[Player:action_check_legitimacy Check whether legitimate] -> open_kachagram

=== open_kachagram ===
+[Player:action_open_kachagram Return to Kachagram chat] -> dialogue_10

=== dialogue_10 ===
+[Player:message Nice I saw your name there. But why you wanna look for me sia? Abit sus.] -> dialogue_11

=== dialogue_11 ===
Haha I just saw your story and found out you're the founder of BeiBei! Wah this is big eh I can't believe you never shared it with me. Anyways, I was thinking... if you ever need any financial advice to help you plan better or manage your money more efficiently you can look for me! #Sender:message

+[Player:message Oh I see] -> dialogue_12

=== dialogue_12 ===
Great. So do you want to proceed? #Sender:message

+[Player:win_ending Ya sure!] -> END