import { Arg, Args, Mutation, Query, Resolver } from "type-graphql";
import { Dummy } from "./dummy";
import { DummyInput } from "./dummy-input";
import { SkipTakeArgs } from "./skip-take-args";

@Resolver(() => Dummy)
export class DummyResolver {
    source: Dummy[] = [];
    
    @Query(() => Dummy)
    dummy(@Arg('id') id: number): Dummy {
        const existingItem = this.source.filter(el => el.id === id)[0];
        if (!existingItem) throw new Error('Item not found');
        return existingItem;
    }

    @Query(() => [Dummy])
    dummies(@Args() { skip, take }: SkipTakeArgs): Dummy[] {
        return this.source.slice(skip, skip + take);
    }

    @Mutation(() => Dummy)
    addDummy(@Arg('input') { name, updatedAt, status }: DummyInput) {
        const lastItem = this.source.sort((a, b) => b.id - a.id)[0];
        const newItem = {
            id: lastItem ? lastItem.id + 1 : 1,
            name,
            updatedAt: updatedAt ? updatedAt : new Date(),
            status,
        };
        this.source.push(newItem);
        return newItem;
    }
}