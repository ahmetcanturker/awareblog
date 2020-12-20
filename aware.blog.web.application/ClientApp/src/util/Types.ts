export type DateTime = Date | string;

export type Nullable<T> = T | null;

export type Optional<T> = T | undefined;

export type NullableString = Nullable<string>;

export type int = number;

export type Callback0<TOut> = () => TOut;

export type Callback1<TOut, TIn> = (param: TIn) => TOut;

export type Func<T> = Callback0<T>;

export type Action<T> = Callback1<void, T>;
