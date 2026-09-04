import { LoaderCircle } from "lucide-react";
import { Button } from "../ui/button";

type ButtonSubmitProps = {
    form: string;
    isPending: boolean;
    text: string;
};
export function ButtonSubmit({ form, isPending, text }: ButtonSubmitProps) {
    return (
        <Button form={form} type="submit" isDisabled={isPending}>
            {isPending && <LoaderCircle className="animate-spin" />}
            {text}
        </Button>
    );
}