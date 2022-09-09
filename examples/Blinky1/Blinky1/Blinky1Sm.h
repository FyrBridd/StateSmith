// Autogenerated with StateSmith
#pragma once
#include <stdint.h>  // this ends up in the generated .h file

enum Blinky1Sm_EventId
{
    Blinky1Sm_EventId_DO = 0, // The `do` event is special. State event handlers do not consume this event (ancestors all get it too) unless a transition occurs.
};

enum
{
    Blinky1Sm_EventIdCount = 1
};

enum Blinky1Sm_StateId
{
    Blinky1Sm_StateId_ROOT = 0,
    Blinky1Sm_StateId_LED_OFF = 1,
    Blinky1Sm_StateId_LED_ON = 2,
};

enum
{
    Blinky1Sm_StateIdCount = 3
};

typedef struct Blinky1Sm Blinky1Sm;
typedef void (*Blinky1Sm_Func)(Blinky1Sm* sm);

struct Blinky1Sm
{
    // Used internally by state machine. Feel free to inspect, but don't modify.
    enum Blinky1Sm_StateId state_id;
    
    // Used internally by state machine. Don't modify.
    Blinky1Sm_Func ancestor_event_handler;
    
    // Used internally by state machine. Don't modify.
    Blinky1Sm_Func current_event_handlers[Blinky1Sm_EventIdCount];
    
    // Used internally by state machine. Don't modify.
    Blinky1Sm_Func current_state_exit_handler;
    
    // User variables. Can be used for inputs, outputs, user variables...
    struct
    {
        uint32_t timer_started_at_ms;  // milliseconds
    } vars;
};

// State machine constructor. Must be called before start or dispatch event functions. Not thread safe.
void Blinky1Sm_ctor(Blinky1Sm* self);

// Starts the state machine. Must be called before dispatching events. Not thread safe.
void Blinky1Sm_start(Blinky1Sm* self);

// Dispatches an event to the state machine. Not thread safe.
void Blinky1Sm_dispatch_event(Blinky1Sm* self, enum Blinky1Sm_EventId event_id);

// Converts a state id to a string. Thread safe.
const char* Blinky1Sm_state_id_to_string(const enum Blinky1Sm_StateId id);

