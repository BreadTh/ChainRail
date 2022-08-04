
namespace BreadTh.ChainRail;

public interface IFutureOutcome<VALUE> : IFutureOutcomeBase
{
    Task<IOutcome<VALUE>> Execute();

    Task Execute(Action<VALUE> onSuccess, Action<IError> onError);
    Task Execute(Action<VALUE> onSuccess, Func<IError, Task> onError);
    Task Execute(Func<VALUE, Task> onSuccess, Action<IError> onError);
    Task Execute(Func<VALUE, Task> onSuccess, Func<IError, Task> onError);



    IFutureOutcome ForgetResult();



    IFutureOutcome<OUTPUT> Then<OUTPUT>(Func<VALUE, OUTPUT> next);
    IFutureOutcome<OUTPUT> Then<OUTPUT>(Func<VALUE, Task<OUTPUT>> next);
    IFutureOutcome Then(Action<VALUE> next);
    IFutureOutcome Then(Func<VALUE, Task> next);

    IFutureOutcome<OUTPUT> Then<OUTPUT>(Func<VALUE, IOutcome<OUTPUT>> next);
    IFutureOutcome<OUTPUT> Then<OUTPUT>(Func<VALUE, Task<IOutcome<OUTPUT>>> next);

    IFutureOutcome Then(Func<VALUE, IOutcome> next);
    IFutureOutcome Then(Func<VALUE, Task<IOutcome>> next);

    IFutureOutcome<OUTPUT> Then<OUTPUT>(Func<VALUE, IFutureOutcome<OUTPUT>> next);
    IFutureOutcome Then(Func<VALUE, IFutureOutcome> next);



    IFutureOutcome<VALUE> Tee<OUTPUT>(Func<VALUE, OUTPUT> next);
    IFutureOutcome<VALUE> Tee<OUTPUT>(Func<VALUE, Task<OUTPUT>> next);
    IFutureOutcome<VALUE> Tee(Action<VALUE> next);
    IFutureOutcome<VALUE> Tee(Func<VALUE, Task> next);

    IFutureOutcome<VALUE> Tee<OUTPUT>(Func<VALUE, IOutcome<OUTPUT>> next);
    IFutureOutcome<VALUE> Tee<OUTPUT>(Func<VALUE, Task<IOutcome<OUTPUT>>> next);
    IFutureOutcome<VALUE> Tee(Func<VALUE, IOutcome> next);
    IFutureOutcome<VALUE> Tee(Func<VALUE, Task<IOutcome>> next);

    IFutureOutcome<VALUE> Tee<OUTPUT>(Func<VALUE, IFutureOutcome<OUTPUT>> next);
    IFutureOutcome<VALUE> Tee(Func<VALUE, IFutureOutcome> next);


    IFutureOutcome<(VALUE, OUTPUT)> Add<OUTPUT>(Func<VALUE, OUTPUT> next);
    IFutureOutcome<(VALUE, OUTPUT)> Add<OUTPUT>(Func<VALUE, Task<OUTPUT>> next);
    IFutureOutcome<(VALUE, OUTPUT)> Add<OUTPUT>(Func<VALUE, IOutcome<OUTPUT>> next);
    IFutureOutcome<(VALUE, OUTPUT)> Add<OUTPUT>(Func<VALUE, Task<IOutcome<OUTPUT>>> next);
    IFutureOutcome<(VALUE, OUTPUT)> Add<OUTPUT>(Func<VALUE, IFutureOutcome<OUTPUT>> next);

    IFutureOutcome<(VALUE, OUTPUT)> Add<OUTPUT>(Func<OUTPUT> next);
    IFutureOutcome<(VALUE, OUTPUT)> Add<OUTPUT>(Func<Task<OUTPUT>> next);
    IFutureOutcome<(VALUE, OUTPUT)> Add<OUTPUT>(Func<IOutcome<OUTPUT>> next);
    IFutureOutcome<(VALUE, OUTPUT)> Add<OUTPUT>(Func<Task<IOutcome<OUTPUT>>> next);
    IFutureOutcome<(VALUE, OUTPUT)> Add<OUTPUT>(Func<IFutureOutcome<OUTPUT>> next);


    IFutureOutcome<OUTPUT> Then<OUTPUT, INPUT_1>(INPUT_1 input1, Func<VALUE, INPUT_1, IFutureOutcome<OUTPUT>> next);
    IFutureOutcome<OUTPUT> Then<OUTPUT, INPUT_1, INPUT_2>(INPUT_1 input1, INPUT_2 input2, Func<VALUE, INPUT_1, INPUT_2, IFutureOutcome<OUTPUT>> next);
    IFutureOutcome<OUTPUT> Then<OUTPUT, INPUT_1, INPUT_2, INPUT_3>(INPUT_1 input1, INPUT_2 input2, INPUT_3 input3, Func<VALUE, INPUT_1, INPUT_2, INPUT_3, IFutureOutcome<OUTPUT>> next);
    IFutureOutcome<OUTPUT> Then<OUTPUT, INPUT_1, INPUT_2, INPUT_3, INPUT_4>(INPUT_1 input1, INPUT_2 input2, INPUT_3 input3, INPUT_4 input4, Func<VALUE, INPUT_1, INPUT_2, INPUT_3, INPUT_4, IFutureOutcome<OUTPUT>> next);
    IFutureOutcome<OUTPUT> Then<OUTPUT, INPUT_1, INPUT_2, INPUT_3, INPUT_4, INPUT_5>(INPUT_1 input1, INPUT_2 input2, INPUT_3 input3, INPUT_4 input4, INPUT_5 input5, Func<VALUE, INPUT_1, INPUT_2, INPUT_3, INPUT_4, INPUT_5, IFutureOutcome<OUTPUT>> next);
    IFutureOutcome<OUTPUT> Then<OUTPUT, INPUT_1, INPUT_2, INPUT_3, INPUT_4, INPUT_5, INPUT_6>(INPUT_1 input1, INPUT_2 input2, INPUT_3 input3, INPUT_4 input4, INPUT_5 input5, INPUT_6 input6, Func<VALUE, INPUT_1, INPUT_2, INPUT_3, INPUT_4, INPUT_5, INPUT_6, IFutureOutcome<OUTPUT>> next);
    IFutureOutcome<OUTPUT> Then<OUTPUT, INPUT_1, INPUT_2, INPUT_3, INPUT_4, INPUT_5, INPUT_6, INPUT_7>(INPUT_1 input1, INPUT_2 input2, INPUT_3 input3, INPUT_4 input4, INPUT_5 input5, INPUT_6 input6, INPUT_7 input7, Func<VALUE, INPUT_1, INPUT_2, INPUT_3, INPUT_4, INPUT_5, INPUT_6, INPUT_7, IFutureOutcome<OUTPUT>> next);
    IFutureOutcome<OUTPUT> Then<OUTPUT, INPUT_1, INPUT_2, INPUT_3, INPUT_4, INPUT_5, INPUT_6, INPUT_7, INPUT_8>(INPUT_1 input1, INPUT_2 input2, INPUT_3 input3, INPUT_4 input4, INPUT_5 input5, INPUT_6 input6, INPUT_7 input7, INPUT_8 input8, Func<VALUE, INPUT_1, INPUT_2, INPUT_3, INPUT_4, INPUT_5, INPUT_6, INPUT_7, INPUT_8, IFutureOutcome<OUTPUT>> next);

    IFutureOutcome<VALUE> Tee<OUTPUT, INPUT_1>(INPUT_1 input1, Func<VALUE, INPUT_1, IFutureOutcome<OUTPUT>> next);
    IFutureOutcome<VALUE> Tee<OUTPUT, INPUT_1, INPUT_2>(INPUT_1 input1, INPUT_2 input2, Func<VALUE, INPUT_1, INPUT_2, IFutureOutcome<OUTPUT>> next);
    IFutureOutcome<VALUE> Tee<OUTPUT, INPUT_1, INPUT_2, INPUT_3>(INPUT_1 input1, INPUT_2 input2, INPUT_3 input3, Func<VALUE, INPUT_1, INPUT_2, INPUT_3, IFutureOutcome<OUTPUT>> next);
    IFutureOutcome<VALUE> Tee<OUTPUT, INPUT_1, INPUT_2, INPUT_3, INPUT_4>(INPUT_1 input1, INPUT_2 input2, INPUT_3 input3, INPUT_4 input4, Func<VALUE, INPUT_1, INPUT_2, INPUT_3, INPUT_4, IFutureOutcome<OUTPUT>> next);
    IFutureOutcome<VALUE> Tee<OUTPUT, INPUT_1, INPUT_2, INPUT_3, INPUT_4, INPUT_5>(INPUT_1 input1, INPUT_2 input2, INPUT_3 input3, INPUT_4 input4, INPUT_5 input5, Func<VALUE, INPUT_1, INPUT_2, INPUT_3, INPUT_4, INPUT_5, IFutureOutcome<OUTPUT>> next);
    IFutureOutcome<VALUE> Tee<OUTPUT, INPUT_1, INPUT_2, INPUT_3, INPUT_4, INPUT_5, INPUT_6>(INPUT_1 input1, INPUT_2 input2, INPUT_3 input3, INPUT_4 input4, INPUT_5 input5, INPUT_6 input6, Func<VALUE, INPUT_1, INPUT_2, INPUT_3, INPUT_4, INPUT_5, INPUT_6, IFutureOutcome<OUTPUT>> next);
    IFutureOutcome<VALUE> Tee<OUTPUT, INPUT_1, INPUT_2, INPUT_3, INPUT_4, INPUT_5, INPUT_6, INPUT_7>(INPUT_1 input1, INPUT_2 input2, INPUT_3 input3, INPUT_4 input4, INPUT_5 input5, INPUT_6 input6, INPUT_7 input7, Func<VALUE, INPUT_1, INPUT_2, INPUT_3, INPUT_4, INPUT_5, INPUT_6, INPUT_7, IFutureOutcome<OUTPUT>> next);
    IFutureOutcome<VALUE> Tee<OUTPUT, INPUT_1, INPUT_2, INPUT_3, INPUT_4, INPUT_5, INPUT_6, INPUT_7, INPUT_8>(INPUT_1 input1, INPUT_2 input2, INPUT_3 input3, INPUT_4 input4, INPUT_5 input5, INPUT_6 input6, INPUT_7 input7, INPUT_8 input8, Func<VALUE, INPUT_1, INPUT_2, INPUT_3, INPUT_4, INPUT_5, INPUT_6, INPUT_7, INPUT_8, IFutureOutcome<OUTPUT>> next);

    IFutureOutcome<VALUE> Tee<INPUT_1>(INPUT_1 input1, Func<VALUE, INPUT_1, IFutureOutcome> next);
    IFutureOutcome<VALUE> Tee<INPUT_1, INPUT_2>(INPUT_1 input1, INPUT_2 input2, Func<VALUE, INPUT_1, INPUT_2, IFutureOutcome> next);
    IFutureOutcome<VALUE> Tee<INPUT_1, INPUT_2, INPUT_3>(INPUT_1 input1, INPUT_2 input2, INPUT_3 input3, Func<VALUE, INPUT_1, INPUT_2, INPUT_3, IFutureOutcome> next);
    IFutureOutcome<VALUE> Tee<INPUT_1, INPUT_2, INPUT_3, INPUT_4>(INPUT_1 input1, INPUT_2 input2, INPUT_3 input3, INPUT_4 input4, Func<VALUE, INPUT_1, INPUT_2, INPUT_3, INPUT_4, IFutureOutcome> next);
    IFutureOutcome<VALUE> Tee<INPUT_1, INPUT_2, INPUT_3, INPUT_4, INPUT_5>(INPUT_1 input1, INPUT_2 input2, INPUT_3 input3, INPUT_4 input4, INPUT_5 input5, Func<VALUE, INPUT_1, INPUT_2, INPUT_3, INPUT_4, INPUT_5, IFutureOutcome> next);
    IFutureOutcome<VALUE> Tee<INPUT_1, INPUT_2, INPUT_3, INPUT_4, INPUT_5, INPUT_6>(INPUT_1 input1, INPUT_2 input2, INPUT_3 input3, INPUT_4 input4, INPUT_5 input5, INPUT_6 input6, Func<VALUE, INPUT_1, INPUT_2, INPUT_3, INPUT_4, INPUT_5, INPUT_6, IFutureOutcome> next);
    IFutureOutcome<VALUE> Tee<INPUT_1, INPUT_2, INPUT_3, INPUT_4, INPUT_5, INPUT_6, INPUT_7>(INPUT_1 input1, INPUT_2 input2, INPUT_3 input3, INPUT_4 input4, INPUT_5 input5, INPUT_6 input6, INPUT_7 input7, Func<VALUE, INPUT_1, INPUT_2, INPUT_3, INPUT_4, INPUT_5, INPUT_6, INPUT_7, IFutureOutcome> next);
    IFutureOutcome<VALUE> Tee<INPUT_1, INPUT_2, INPUT_3, INPUT_4, INPUT_5, INPUT_6, INPUT_7, INPUT_8>(INPUT_1 input1, INPUT_2 input2, INPUT_3 input3, INPUT_4 input4, INPUT_5 input5, INPUT_6 input6, INPUT_7 input7, INPUT_8 input8, Func<VALUE, INPUT_1, INPUT_2, INPUT_3, INPUT_4, INPUT_5, INPUT_6, INPUT_7, INPUT_8, IFutureOutcome> next);


}