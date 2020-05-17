I'm Sally the Mako Shark!
    +   [...]
        I've been waiting for someone to come find me,
        ++  [...]
            Been waiting a long time...
            +++ [...]
            Name
                ++++    [Yes.. That's me!]
                -> AgreeWithName
                ++++    [How do you know my name?]
                -> ForgetName
                ++++    [No, that's not my name.]
                -> ForgetName
                        
=== ForgetName ===
You told me, don't you remember silly?
    + [Oh yes I remember.]
    -> AgreeWithName
    + [Sorry I don't.]
      Do you want me to remind you?
      ++[Yes.]
            Back to the start to you.
            +++[Wait no]
            Backwards
            -> END
            +++[Okay]
            Backwards
            -> END
      ++[No.]
      Glad we got that sorted! and
      ->AgreeWithName
      
    + [Why would I, I never told you.]
        Do you want me to remind you?
      ++[Yes.]
            Back to the start to you.
            +++[Wait no]
            Backwards
            -> END
            +++[Okay]
            Backwards
            -> END
      ++[No.]
      Glad we got that sorted! and
      ->AgreeWithName
        
    
=== AgreeWithName ===
Yaya! I thought I got it right!
    + [...]
    Now I'm starving!
        ++ [...]
            Will you come hunting with me?
            ***   [Yes I'll hunt with you. ]
                -> Yes
            ***   [No, I'm sure you are capable. ]
                -> No 
            +++   [Why do you need help?]
                        
=== Yes ===
Yes! I'm so happy! #happy
 + [...]
    We're gonna be best friends me and you.
        ++[Yes we will.]
        Awe, So sweet.
            +++[...]
            Lead the way!
            ->DONE
        ++[We're already best friends.]
        Awe, So sweet.
            +++[...]
            Lead the way!
            ->DONE
        ++[Unsure about that.]
            I Didn't want to be your friend anyway.
            +++ [...]
                Lead the way!
                ->DONE
        ++[Doubt it]
        -> No

=== No ===
That's fine, I can hunt by myself anyway.
+[...]
Boo
->END

=== SheMad ===
What do you want?
    +[Sorry for upsetting you.]
        -> WillHelpHunt
    +[How's it going?]
        I'm hunting. Don't bother me.
        ++[Well it's great hunting!]
            Really?
            +++[Yes, you are really good]
            Thank you.
            ->WillHelpHunt
            +++[Yeah random directions always works...]
                okay... What's that surpose to mean?
                ++++[That you know what your doing!]
                    Right...
                    +++++ [...]
                    LeaveNow
                    -> END
                ++++[I'm saying you don't know how to hunt.]
                LeaveNow
                -> END
        ++[You are going in random directions]
            And? That has nothing to do with you. Go Away.
            +++ [...]
            LeaveNow
            -> END
            
    +[Do you even know where you are going?]
        Don't bother me. I know how to hunt.
        ++[Do you want any help?]
            Why would you care.
            +++[This time is different]
            -> WillHelpHunt
            +++[I'm your friend remember?]
            -> WillHelpHunt
        ++[Okay]
        LeaveNow
        -> END
                
=== WillHelpHunt===
Does this mean you will help me hunt?
    +[Yes I will help you!]
        Thank you, Now lead the way.
            ++[Okay]
            Lead the way!
             ->DONE
    +[No, this changes nothing.]
        Leave me alone.
            ++[...]
            LeaveNow
            ->END