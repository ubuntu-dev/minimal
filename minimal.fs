\ Minimal Forth word set                                         uh 2015-10-05

: tick (  <spaces>name<spaces> -- xt f )
    state @  ] bl word find rot IF ] ELSE POSTPONE [ THEN ;

: immediate-alias ( xt <spaces>name<spaces> -- )
    CREATE , immediate DOES> @ EXECUTE ;

: non-immediate-alias ( xt <spaces>name<spaces> -- )
    CREATE , immediate DOES> @ state @ IF compile,  ELSE  EXECUTE THEN ;

variable #primitives  0 #primitives !
variable #words 0 #words !

: another-primitive ( -- )  1 #primitives +!  1 #words +! ;

: primitive ( <space>ccc<space> -- )
    get-order
    forth-wordlist 1 set-order
    another-primitive
    >in @  tick  rot >in !  0< IF non-immediate-alias ELSE immediate-alias THEN
    set-order ;


wordlist Constant minimal

minimal set-current

primitive !
primitive ,
primitive @
primitive ALIGN
\ primitive ALIGNED
\ primitive CELL+
primitive CELLS
primitive C!
primitive C,
primitive C@
: CALIGN ;   another-primitive
: CALIGNED ; another-primitive
\ primitive CHAR+
primitive CHARS

\ primitive + 
\ primitive *
\ primitive 2*
primitive */MOD
primitive -
\ primitive /
\ primitive 2/
\ primitive MOD

\ primitive 0=
primitive <
primitive AND
primitive INVERT
\ primitive TRUE
\ primitive LSHIFT
\ primitive =
\ primitive >
\ primitive OR
\ primitive XOR
\ primitive FALSE
primitive RSHIFT

\ primitive DUP
primitive SWAP
primitive >R
primitive R@
primitive DROP
primitive OVER
primitive R>

primitive IF
primitive THEN
primitive WHILE
primitive REPEAT
primitive DO
primitive I
primitive '
primitive ELSE
primitive BEGIN
primitive AGAIN
primitive UNTIL
primitive LOOP
primitive J
primitive EXECUTE

\ primitive :
: : : 1 #words +! ; another-primitive
\ primitive CONSTANT
\ primitive CREATE
: CREATE  CREATE 1 #words +! ; another-primitive
primitive ;
\ primitive VARIABLE
primitive DOES>

primitive KEY
primitive EMIT
primitive KEY?
primitive CR

primitive (
primitive .S
primitive \

: bye bye ;
: INCLUDE include ;
\ primitive WORDS
: WORDS  #primitives @ . ." primitives, " #words @ . ." words"  WORDS ;
\ : order  order ;

\ support for compiling words
\ primitive POSTPONE
\ primitive IMMEDIATE
\ primitive >BODY
\ primitive SLITERAL
\ primitive LITERAL
\ primitive COMPILE,

\ support for processing the input stream
\ primitive PARSE

\ primitive EXIT

: primitive primitive ;


get-order ' set-order
minimal 1 set-order 

include prelude.fs

execute
cr .( Minimal Forth word set activated: )  #primitives @ . .( primitives, ) #words @ . .( words) cr
minimal 1 set-order
