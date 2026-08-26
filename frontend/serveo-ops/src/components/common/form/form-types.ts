import type { FieldPathByValue, FieldValues } from 'react-hook-form';

export type StringFieldPath<T extends FieldValues> = FieldPathByValue<T, string>;

export type BooleanFieldPath<T extends FieldValues> = FieldPathByValue<T, boolean>;

export type NumberFieldPath<T extends FieldValues> = FieldPathByValue<T, number>;
