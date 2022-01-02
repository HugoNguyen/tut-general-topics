import { Field, InputType } from "type-graphql";
import { DummyStatus } from "./dummy-status-enum";

@InputType()
export class DummyInput {
    @Field()
    name: string;
    @Field({nullable: true})
    updatedAt?: Date;
    @Field(() => DummyStatus)
    status: DummyStatus = DummyStatus.DRAFT;
}
